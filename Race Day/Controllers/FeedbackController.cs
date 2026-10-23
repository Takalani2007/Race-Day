using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
