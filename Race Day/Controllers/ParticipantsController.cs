using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly RaceEventApiService _api;

        public ParticipantsController(RaceEventApiService api)
        {
            _api = api;
        }

        // 1. READ ALL
        public async Task<IActionResult> Index()
        {
            var participants = await _api.GetAllParticipantsAsync();
            return View(participants);
        }

        // 2. READ ONE (Details)
        public async Task<IActionResult> Details(int id)
        {
            var participant = await _api.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        // 3. CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant participant)
        {
            if (ModelState.IsValid)
            {
                if (await _api.CreateParticipantAsync(participant))
                {
                    TempData["Success"] = "Participant added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to add participant. Make sure the Event ID and User ID are valid.");
            }
            return View(participant);
        }

        // 4. UPDATE (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var participant = await _api.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        // 4. UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Participant participant)
        {
            if (id != participant.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _api.UpdateParticipantAsync(id, participant))
                {
                    TempData["Success"] = "Participant updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update participant.");
            }
            return View(participant);
        }

        // 5. DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _api.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        // 5. DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _api.DeleteParticipantAsync(id))
            {
                TempData["Success"] = "Participant removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}