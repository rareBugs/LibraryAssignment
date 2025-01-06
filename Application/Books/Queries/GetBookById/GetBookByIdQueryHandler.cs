using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookbyIdQueryHandler : IRequestHandler <GetBookbyIdQuery, OperationResults<Book>>
    {
        private readonly IGenericRepository<Book> _genericRepository;

        public GetBookbyIdQueryHandler(IGenericRepository<Book> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<Book>> Handle(GetBookbyIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _genericRepository.GetByIdAsync(request.BookId);

            if (book == null)
                return OperationResults<Book>.FailureResult("Book not found.");
            
            return OperationResults<Book>.SuccessResult(book);
        }
    }
}