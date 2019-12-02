using HappyDog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
