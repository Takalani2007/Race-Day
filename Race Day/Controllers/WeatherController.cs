using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
