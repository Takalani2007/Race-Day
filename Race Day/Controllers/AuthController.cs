using Microsoft.AspNetCore.Mvc;
using Race_Day.Models;
using Race_Day.Services;

namespace Race_Day.Controllers
{
    public class AuthController : Controller
    {
        private readonly RaceEventApiService _api;

        public AuthController(RaceEventApiService api)
        {
            _api = api;
        }
      

        // --- REGISTER ---

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _api.RegisterUserAsync(user))
                {
                    TempData["Success"] = "Registration successful! Please log in.";
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError("", "Failed to register. Email might already be in use.");
            }
            return View(user);
        }

    

        // --- LOGIN ---

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and password are required.");
                return View();
            }

            if (await _api.LoginAsync(email, password))
            {
                TempData["Success"] = "Logged in successfully!";
                // In a real app, you would save the token or user session here
                return RedirectToAction("Index", "Events");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // --- LOGOUT ---

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session/token here if implemented
            TempData["Success"] = "Logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

        // --- USER MANAGEMENT (Admin View) ---

        public async Task<IActionResult> Users()
        {
            var users = await _api.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _api.DeleteUserAsync(id))
            {
                TempData["Success"] = "User deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete user.";
            }

            return RedirectToAction(nameof(Users));
        }
    }
}