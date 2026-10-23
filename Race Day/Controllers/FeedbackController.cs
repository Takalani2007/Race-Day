using Microsoft.AspNetCore.Mvc;

namespace Race_Day.Controllers
{
public class FeedbackController : Controller
    {
        private readonly RaceEventApiService _api;
        public FeedbackController(RaceEventApiService api) => _api = api;

        private int CurrentUserId => 2; 

        public async Task<IActionResult> Index() => View(await _api.GetAllFeedbackAsync());

        [HttpGet]
        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int eventId, string comment, int rating)
        {
            if (await _api.SubmitFeedbackAsync(eventId, CurrentUserId, comment, rating))
                TempData["Success"] = "Feedback submitted!";
            return RedirectToAction("Details", "Events", new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteFeedbackAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
