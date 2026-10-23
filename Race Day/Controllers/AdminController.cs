using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
   public class AdminController : Controller
    {
        private readonly RaceEventApiService _api;
        public AdminController(RaceEventApiService api) => _api = api;

        // --- Weather ---
        public async Task<IActionResult> Weather() => View(await _api.GetAllWeatherAsync());

        [HttpPost]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            await _api.DeleteWeatherAsync(id);
            return RedirectToAction(nameof(Weather));
        }

        // --- Results ---
        public async Task<IActionResult> Results() => View(await _api.GetAllResultsAsync());

        [HttpPost]
        public async Task<IActionResult> DeleteResult(int id)
        {
            await _api.DeleteResultAsync(id);
            return RedirectToAction(nameof(Results));
        }

        // --- Participants ---
        public async Task<IActionResult> Participants() => View(await _api.GetAllParticipantsAsync());

        [HttpPost]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            await _api.DeleteParticipantAsync(id);
            return RedirectToAction(nameof(Participants));
        }
    }
}
