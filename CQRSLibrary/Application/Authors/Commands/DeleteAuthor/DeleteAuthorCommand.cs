using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public DeleteAuthorCommand(Guid authorId)
        {
            AuthorId = authorId;
        }

        public Guid AuthorId { get; set; }
    }
}