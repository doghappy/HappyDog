using HappyDog.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using HappyDog.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class UserService
    {
        readonly HappyDogContext db;

        public UserService(HappyDogContext db)
        {
            this.db = db;
        }

        public async Task<DataResult<User>> LoginAsync(LoginDto dto)
        {
            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == dto.UserName);
            return new DataResult<User>
            {
                Result = user != null && user.Password == HashEncrypt.Sha1Encrypt($"HappyDog-{dto.Password}-{user.PasswordHash}"),
                Data = user
            };
        }
    }
}
