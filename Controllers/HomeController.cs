using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RefreshTokensWebApiExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        } 

        public IActionResult Index()
        {
            return View(int.Parse(_configuration["accessTokenDurationInMinutes"]));
        }

        [Authorize]
        public IActionResult Test()
        {
            return Content($"The user: {User.Identity.Name} made an authenticated call at {DateTime.Now.ToString("HH:mm:ss")}", "text/plain");
        }
    }
}