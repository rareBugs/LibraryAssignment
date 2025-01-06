using Microsoft.AspNetCore.Identity;

namespace Database.Authentication
{
    public class SecurityService : ISecurityService
    {
        private readonly IPasswordHasher<object> _passwordHasher;
        private static readonly object HasherContext = new();

        public SecurityService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(HasherContext, password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(HasherContext, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}