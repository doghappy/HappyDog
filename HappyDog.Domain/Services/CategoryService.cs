using HappyDog.Domain.Entities;
using HappyDog.Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(HappyDogContext db)
        {
            this.db = db;
        }

        readonly HappyDogContext db;

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await db.Categories.AsNoTracking().ToListAsync();
        }
    }
}
