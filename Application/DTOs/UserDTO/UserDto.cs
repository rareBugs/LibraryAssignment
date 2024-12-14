namespace Application.DTOs.UserDto
{
    public class UserDto
    {
        public required string UserName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}