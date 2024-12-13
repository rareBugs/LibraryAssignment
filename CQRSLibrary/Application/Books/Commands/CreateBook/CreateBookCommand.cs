using Domain.Models;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Book>
    {

        public CreateBookCommand(Book bookToAdd)
        {
            NewBook = bookToAdd;
        }

        public Book NewBook { get; set; }
    }
}