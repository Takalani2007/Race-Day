using Microsoft.AspNetCore.Mvc;
using Race_Day.Services;
using Race_Day.Models;
namespace Race_Day.Controllers
{
public class AuthController : Controller
    {
        private readonly RaceEventApiService _api;
        public AuthController(RaceEventApiService api) => _api = api;

            [HttpGet]
            public IActionResult Login() => View();

            [HttpPost]
            public async Task<IActionResult> Login(string email, string password)
            {
                if (await _api.LoginAsync(email, password))
                {
                    TempData["Success"] = "Logged in successfully!";
                    return RedirectToAction("Index", "Events");
                }
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }

            public async Task<IActionResult> Users() => View(await _api.GetAllUsersAsync());

            public async Task<IActionResult> DeleteUser(int id)
            {
                await _api.DeleteUserAsync(id);
                return RedirectToAction(nameof(Users));
            }
        }
    }
