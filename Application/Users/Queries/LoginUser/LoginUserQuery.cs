using Application.DTOs.UserDto;
using Domain.Models;
using MediatR;


namespace Application.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<OperationResults<string>>
    {
        public UserDto LoginUserDto { get; }

        public LoginUserQuery(UserDto loginUserDto)
        {
            LoginUserDto = loginUserDto;
        }
    }
}