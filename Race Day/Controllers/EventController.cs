using Microsoft.AspNetCore.Mvc;
using Race_Day3.Services;

namespace Race_Day3.Controllers
{
    public class EventsController : Controller
    {
        private readonly RaceEventApiService _eventApi;

        public EventsController(RaceEventApiService eventApi)
        {
            _eventApi = eventApi;
        }

        // READ: Display all events
        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _eventApi.GetAllEventsAsync();
                return View(events);
            }
            catch (HttpRequestException)
            {
                return View("ApiError");
            }
        }

        // READ: Display event details
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventApi.GetEventByIdAsync(id);
            if (ev == null) return NotFound();

            return View(ev);
        }
    }
}