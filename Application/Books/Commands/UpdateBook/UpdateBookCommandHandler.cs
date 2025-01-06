using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResults<Book>>
    {
        private readonly IGenericRepository<Book> _genericRepository;

        public UpdateBookCommandHandler(IGenericRepository<Book> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booktoUpdate = await _genericRepository.GetByIdAsync(request.BookId);

                if (booktoUpdate == null)
                    return OperationResults<Book>.FailureResult("Book not found.");

                booktoUpdate.Title = request.UpdateBookDto.Title;
                booktoUpdate.Description = request.UpdateBookDto.Description;

                await _genericRepository.UpdateAsync(booktoUpdate);

                return OperationResults<Book>.SuccessResult(booktoUpdate);
            }

            catch (Exception ex)
            {
                return OperationResults<Book>.FailureResult("Error while updating book.");
            }
        }
    }
}