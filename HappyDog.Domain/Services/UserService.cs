using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
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

        public async Task<BaseResult> LoginAsync(LoginDto dto)
        {
            var user = await db.Users.AsNoTracking()
                .Include(u=>u.UserRoles)
                .SingleOrDefaultAsync(u => u.UserName == dto.UserName && u.Password == dto.Password);
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
    }
}
