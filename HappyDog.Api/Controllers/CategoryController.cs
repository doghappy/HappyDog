using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.DataTransferObjects.Category;
using HappyDog.Domain;
using HappyDog.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        readonly HappyDogContext context;

        public CategoryController(HappyDogContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<CategoryDTO>> Get()
        {
            return await context.Categories.AsNoTracking()
                 .Where(c => c.State == BaseState.Enable)
                 .Select(c => new CategoryDTO
                 {
                     Id = c.Id,
                     Label = c.Label,
                     Value = c.Value,
                     Color = c.Color,
                     IconClass = c.IconClass
                 })
                 .ToListAsync();
        }
    }
}