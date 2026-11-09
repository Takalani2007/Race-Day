using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
    public class EnrolmentsController : Controller
    {
        private readonly RaceEventApiService _api;

        public EnrolmentsController(RaceEventApiService api)
        {
            _api = api;
        }

        // Hardcoded UserId 2 (John Doe) for demo
        private int CurrentUserId => 2;

        // 1. READ ALL
        public async Task<IActionResult> Index()
        {
            var enrolments = await _api.GetAllEnrolmentsAsync();
            return View(enrolments);
        }
        public async Task<IActionResult> MyEnrolments()
        {
            var myEnrolments = await _api.GetMyEnrolmentsAsync(CurrentUserId);
            return View("Index", myEnrolments); // This will load the Index.cshtml view instead!
        }
        // 3. READ ONE (Details)
        public async Task<IActionResult> Details(int id)
        {
            var enrolment = await _api.GetEnrolmentByIdAsync(id);
            if (enrolment == null) return NotFound();
            return View(enrolment);
        }

        // 4. CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 4. CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                if (await _api.EnrolAsync(enrolment.UserId, enrolment.EventId))
                {
                    TempData["Success"] = "Enrolment created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create enrolment. Check if User and Event exist.");
            }
            return View(enrolment);
        }

        // 5. UPDATE (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var enrolment = await _api.GetEnrolmentByIdAsync(id);
            if (enrolment == null) return NotFound();
            return View(enrolment);
        }

        // 5. UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrolment enrolment)
        {
            if (id != enrolment.EnrolmentId) return NotFound();

            if (ModelState.IsValid)
            {
                if (await _api.UpdateEnrolmentAsync(id, enrolment))
                {
                    TempData["Success"] = "Enrolment updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update enrolment.");
            }
            return View(enrolment);
        }

        // 6. DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var enrolment = await _api.GetEnrolmentByIdAsync(id);
            if (enrolment == null) return NotFound();
            return View(enrolment);
        }

        // 6. DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _api.CancelEnrolmentAsync(id))
            {
                TempData["Success"] = "Enrolment deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}