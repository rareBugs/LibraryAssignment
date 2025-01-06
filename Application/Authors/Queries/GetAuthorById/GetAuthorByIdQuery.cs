using Domain.Models;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest <OperationResults<Author>>
    {
        public Guid AuthorId { get; set; }

        public GetAuthorByIdQuery(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}