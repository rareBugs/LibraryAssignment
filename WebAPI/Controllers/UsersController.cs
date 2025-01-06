using Application.DTOs.UserDto;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public UsersController(IMediator mediator, ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/User/GetAllUsers
        [HttpGet("GetAllUsers")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Start processing GetAllUsers request.");
            try
            {
                var result = await _mediator.Send(new GetAllUsersQuery());
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved all users. Count: {UserCount}", result.Data.Count);
                    return Ok(new { message = result.Message, data = result.Data });
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve all users. Reason: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while retrieving all users.");
                return HandleException(ex);
            }
        }

        // POST: api/User/RegisterUser
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto newUser)
        {
            _logger.LogInformation("Processing request to register a new user.");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("RegisterUser: The provided user data is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new AddNewUserCommand(newUser));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully registered a new user with ID: {UserId}", result.Data?.Id);
                    return CreatedAtAction(nameof(GetAllUsers), new { id = result.Data?.Id }, result.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to register a new user. Reason: {ErrorMessage}", result.ErrorMessage);
                    return BadRequest(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering a new user.");
                return HandleException(ex);
            }
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDto userWantingToLogin)
        {
            _logger.LogInformation("Processing request to log in a user.");
            try
            {
                if (userWantingToLogin == null)
                {
                    _logger.LogWarning("LoginUser: The provided user data is null.");
                    return BadRequest(new { message = "Login data cannot be null." });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("LoginUser: The provided user data is invalid.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new LoginUserQuery(userWantingToLogin));
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully logged in user: {UserName}", userWantingToLogin.UserName);
                    return Ok(new { message = result.Message, data = result.Data });
                }
                else
                {
                    _logger.LogWarning("Failed login attempt for user: {UserName}. Reason: {ErrorMessage}",
                        userWantingToLogin.UserName, result.ErrorMessage);
                    return Unauthorized(new { message = result.Message, error = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user login.");
                return HandleException(ex);
            }
        }

        // Private error handler
        private IActionResult HandleException(Exception ex)
        {
            _logger.LogError("Internal server error: {ExceptionMessage}", ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Internal server error.", error = ex.Message });
        }
    }
}