using Domain.Models;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<OperationResults<Book>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }

        public CreateBookCommand(string title, string description, Guid authorId)
        {
            Title = title;
            Description = description;
            AuthorId = authorId;
        }
    }
}