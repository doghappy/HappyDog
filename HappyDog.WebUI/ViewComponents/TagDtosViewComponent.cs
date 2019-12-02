using HappyDog.Domain.DataTransferObjects.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyDog.WebUI.ViewComponents
{
    public class TagDtosViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ICollection<TagDto> tags)
        {
            return View(tags);
        }
    }
}
