using Microsoft.AspNetCore.Mvc;

namespace RefreshTokensWebApiExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}