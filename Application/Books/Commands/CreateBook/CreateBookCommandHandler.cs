using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OperationResults<Book>>
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Author> _authorRepository;

        public CreateBookCommandHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<OperationResults<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.FindByAsync(x => x.Id == request.AuthorId);

            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
                return OperationResults<Book>.FailureResult("Please enter title and description.");

            if (author == null)
                return OperationResults<Book>.FailureResult("Author not found.");
            
            try
            {
                var book = new Book(request.Title, request.Description, request.AuthorId);

                await _bookRepository.AddAsync(book);

                return OperationResults<Book>.SuccessResult(book);
            }

            catch (Exception ex)
            {
                return OperationResults<Book>.FailureResult("An error occurred while creating the book.");
            }
        }
    }
}