using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Controllers
{
    public interface ISearchable
    {
        Task<IActionResult> Search(string q, int page = 1);
    }
}
