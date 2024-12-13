using Database.Databases;
using Domain.Models;
using MediatR;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllBooksQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_fakeDatabase.Books);
            }

            catch
            {
                throw new Exception("Books not found");
            }
        }
    }
}