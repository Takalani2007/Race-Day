using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
