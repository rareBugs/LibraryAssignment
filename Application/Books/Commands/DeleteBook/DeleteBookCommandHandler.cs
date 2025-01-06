using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResults<Book>>
    {
        private readonly IGenericRepository<Book> _bookRepository;

        public DeleteBookCommandHandler(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResults<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookToDelete = await _bookRepository.GetByIdAsync(request.BookId);

                if (bookToDelete == null)
                    return OperationResults<Book>.FailureResult("Book not found.");

                await _bookRepository.DeleteAsync(bookToDelete);

                return OperationResults<Book>.SuccessResult(bookToDelete);
            }

            catch (Exception ex)
            {
                return OperationResults<Book>.FailureResult("An error occurred while deleting the book.");
            }
        }
    }
}