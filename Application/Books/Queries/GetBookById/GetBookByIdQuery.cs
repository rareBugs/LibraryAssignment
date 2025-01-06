using Domain.Models;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookbyIdQuery : IRequest<OperationResults<Book>>
    {
        public Guid BookId { get; }

        public GetBookbyIdQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }
}