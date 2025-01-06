using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResults<List<User>>>
    {
        private readonly IGenericRepository<User> _repository;

        public GetAllUsersQueryHandler(IGenericRepository<User> Repository)
        {
            _repository = Repository;
        }

        public async Task<OperationResults<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            if (users == null)
                return OperationResults<List<User>>.FailureResult("Operation failed");

            return OperationResults<List<User>>.SuccessResult(users.ToList());
        }
    }
}