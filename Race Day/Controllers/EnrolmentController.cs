using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class EnrolmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
