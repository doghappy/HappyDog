using System;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class UserService
    {
        public UserService(HappyDogContext db)
        {
            this.db = db;
        }

        readonly HappyDogContext db;

        public async Task<BaseResult> LoginAsync(string userName, string password)
        {
            var user = await db.Users.AsNoTracking()
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(u =>
                    u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                    && u.PasswordHash == password);
            if (user == null)
            {
                return new BaseResult
                {
                    Result = false,
                    Message = "密码错误"
                };
            }
            else
            {
                return new DataResult<User>
                {
                    Result = user != null,
                    Data = user,
                    Message = "登录成功"
                };
            }
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await db.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
