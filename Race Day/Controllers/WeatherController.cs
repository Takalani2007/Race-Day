using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
    public class WeatherController : Controller
    {
        private readonly RaceEventApiService _api;
        public WeatherController(RaceEventApiService api) => _api = api;

        public async Task<IActionResult> Index() => View(await _api.GetAllWeatherAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Weather w)
        {
            if (await _api.CreateWeatherAsync(w)) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Failed to add weather.");
            return View(w);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteWeatherAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
