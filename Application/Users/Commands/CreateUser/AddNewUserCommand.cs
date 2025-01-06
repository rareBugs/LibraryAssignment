using Application.DTOs.UserDto;
using Domain.Models;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class AddNewUserCommand : IRequest<OperationResults<User>>
    {
        public UserDto UserDto { get; set; }

        public AddNewUserCommand(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}