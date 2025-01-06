using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResults<List<Book>>>
    {
        private readonly IGenericRepository<Book> _genericRepository;

        public GetAllBooksQueryHandler(IGenericRepository<Book> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<OperationResults<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _genericRepository.GetAllAsync();

            if (books == null || !books.Any())
                return OperationResults<List<Book>>.FailureResult("Books not found.");

            return OperationResults<List<Book>>.SuccessResult(books.ToList());
        }
    }
}