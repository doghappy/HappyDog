using HappyDog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class CategoryService
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
