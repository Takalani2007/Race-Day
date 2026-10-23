using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
    
    public class EnrolmentsController : Controller
    {
        private readonly RaceEventApiService _api;
        public EnrolmentsController(RaceEventApiService api) => _api = api;

        // Hardcoded UserId 2 (John Doe) for demo
        private int CurrentUserId => 2; 

        public async Task<IActionResult> Index() => View(await _api.GetAllEnrolmentsAsync());
        public async Task<IActionResult> MyEnrolments() => View(await _api.GetMyEnrolmentsAsync(CurrentUserId));

        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId)
        {
            if (await _api.EnrolAsync(CurrentUserId, eventId)) TempData["Success"] = "Enrolled successfully!";
            else TempData["Error"] = "Failed to enrol.";
            return RedirectToAction("Index", "Events");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            if (await _api.CancelEnrolmentAsync(id)) TempData["Success"] = "Enrolment cancelled.";
            return RedirectToAction(nameof(MyEnrolments));
        }
    }
}
