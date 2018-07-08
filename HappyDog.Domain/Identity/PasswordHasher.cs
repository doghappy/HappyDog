using HappyDog.Domain.Entities;
using HappyDog.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;

namespace HappyDog.Domain.Identity
{
    public class PasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return HashEncryptor.HmacSha1(user.SecurityStamp, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (HashPassword(user, providedPassword) == hashedPassword)
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
