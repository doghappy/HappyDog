using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface IUserService
    {
        Task<BaseResult> LoginAsync(string userName, string password);
        Task<User> GetUserAsync(string userName);
    }
}
