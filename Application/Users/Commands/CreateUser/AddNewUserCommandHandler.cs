using Database.Authentication;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, OperationResults< User>>
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly ISecurityService _passwordService;

        public AddNewUserCommandHandler(IGenericRepository<User> genericRepository, ISecurityService passwordService)
        {
            _genericRepository = genericRepository;
            _passwordService = passwordService;
        }

        public async Task<OperationResults<User>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserDto.UserName) || string.IsNullOrWhiteSpace(request.UserDto.Password))
            {
                return OperationResults<User>.FailureResult("Username and password are required");
            }

            string passwordHash = _passwordService.HashPassword(request.UserDto.Password);

            User userToCreate = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserDto.UserName,
                Password = passwordHash
            };
            await _genericRepository.AddAsync(userToCreate);
            return OperationResults<User>.SuccessResult(userToCreate);

        }
    }
}