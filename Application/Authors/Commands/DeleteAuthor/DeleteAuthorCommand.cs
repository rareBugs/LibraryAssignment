using Domain.Models;
using MediatR;

namespace Application.Authors.Commands.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<OperationResults<Author>>
    {
        public Guid AuthorId { get;  }

        public DeleteAuthorCommand(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}