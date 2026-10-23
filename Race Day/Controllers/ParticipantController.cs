using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class ParticipantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
