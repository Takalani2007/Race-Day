using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers

{
public class ResultsController : Controller
    {
        private readonly RaceEventApiService _api;
        public ResultsController(RaceEventApiService api) => _api = api;

        public async Task<IActionResult> Index() => View(await _api.GetAllResultsAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Result r)
        {
            if (await _api.CreateResultAsync(r)) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Failed to add result.");
            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteResultAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
