using Domain.Models;
using MediatR;

namespace Application.Authors.Queries.GetAllauthors
{
    public class GetAllAuthorsQuery : IRequest<OperationResults<List<Author>>> { }
}