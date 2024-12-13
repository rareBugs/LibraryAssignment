using Domain.Models;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookbyIdQuery : IRequest<Book>
    {
        public GetBookbyIdQuery(Guid bookId)
        {
            BookId = bookId;
        }

        public Guid BookId { get; }
    }


}
