using Domain.Models;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest <OperationResults<Book>>
    {
        public Guid BookId { get; set; }

        public DeleteBookCommand(Guid bookId)
        {
            BookId = bookId;
        }        
    }
}