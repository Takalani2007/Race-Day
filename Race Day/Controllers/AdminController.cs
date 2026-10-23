using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
