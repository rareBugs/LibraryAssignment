using Application.DTOs.BookDto;
using Domain.Models;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public Guid BookId { get; set; }
        public UpdateBookDto UpdateBookDto { get; }
        public UpdateBookCommand(Guid bookId, UpdateBookDto updateBookDto)
        {
            BookId = bookId;
            UpdateBookDto = updateBookDto;
        }
    }
}