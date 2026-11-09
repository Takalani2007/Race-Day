using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
    public class UserController : Controller
    {
        private readonly RaceEventApiService _api;

        public UserController(RaceEventApiService api)
        {
            _api = api;
        }

        // 1. READ ALL
        public async Task<IActionResult> Index()
        {
            var users = await _api.GetAllUsersAsync();
            return View(users);
        }

        // 2. READ ONE (Details)
        public async Task<IActionResult> Details(int id)
        {
            var user = await _api.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // 3. UPDATE (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _api.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // 3. UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _api.UpdateUserAsync(id, user))
                {
                    TempData["Success"] = "User updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update user.");
            }
            return View(user);
        }

        // 4. DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _api.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // 4. DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _api.DeleteUserAsync(id))
            {
                TempData["Success"] = "User deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}