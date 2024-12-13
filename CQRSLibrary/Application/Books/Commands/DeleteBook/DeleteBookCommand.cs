using Domain.Models;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public DeleteBookCommand(Guid bookId)
        {
            BookId = bookId;
        }

        public Guid BookId { get; }
    }
}