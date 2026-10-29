using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day3.Controllers
{
    public class EventsController : Controller
    {
        private readonly RaceEventApiService _api;
        public EventsController(RaceEventApiService api) => _api = api;

        public async Task<IActionResult> Index() => View(await _api.GetAllEventsAsync());

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _api.GetEventByIdAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            if (!ModelState.IsValid) return View(ev);
            if (await _api.CreateEventAsync(ev)) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Error creating event.");
            return View(ev);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ev = await _api.GetEventByIdAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event ev)
        {
            if (!ModelState.IsValid) return View(ev);
            if (await _api.UpdateEventAsync(id, ev)) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Error updating event.");
            return View(ev);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _api.GetEventByIdAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}