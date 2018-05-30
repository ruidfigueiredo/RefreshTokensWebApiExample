using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefreshTokensWebApiExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Test()
        {
            return Content($"The user: {User.Identity.Name} made an authenticated call at {DateTime.Now.ToString("HH:mm:ss")}", "text/plain");
        }
    }
}