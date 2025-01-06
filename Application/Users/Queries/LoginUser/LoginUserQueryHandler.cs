using Application.Users.Queries.LoginUser.Helpers;
using Database.Authentication;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Queries.LoginUser
{
    public class LoginUserqueryHandler : IRequestHandler<LoginUserQuery, OperationResults <string>>
    {
        private readonly IGenericRepository<User> _userRrepository;
        private readonly ISecurityService _securityService;
        private readonly TokenHelper _tokenHelper;

        public LoginUserqueryHandler(IGenericRepository<User> userRespository, ISecurityService securityService, TokenHelper tokenHelper)
        {
            _userRrepository = userRespository;
            _securityService = securityService;
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResults<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRrepository.FindByAsync(u => u.UserName == request.LoginUserDto.UserName);

            if (user == null || !_securityService.VerifyPassword(request.LoginUserDto.Password, user.Password))
                return OperationResults<string>.FailureResult("Invalid username or password");


            string token = _tokenHelper.GenerateJwtToken(user);

            return OperationResults<string>.SuccessResult(token);

        }
    }
}