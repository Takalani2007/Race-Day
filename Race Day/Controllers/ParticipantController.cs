using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
  public class ParticipantsController : Controller
    {
        private readonly RaceEventApiService _api;
        public ParticipantsController(RaceEventApiService api) => _api = api;

        public async Task<IActionResult> Index() => View(await _api.GetAllParticipantsAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Participant p)
        {
            if (await _api.CreateParticipantAsync(p)) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Failed to add participant.");
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteParticipantAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
