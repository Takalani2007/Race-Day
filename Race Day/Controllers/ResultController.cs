using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
