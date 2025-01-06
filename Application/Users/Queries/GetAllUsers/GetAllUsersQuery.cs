using Domain.Models;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<OperationResults<List<User>>> { }
}