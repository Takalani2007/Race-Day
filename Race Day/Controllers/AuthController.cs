using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
