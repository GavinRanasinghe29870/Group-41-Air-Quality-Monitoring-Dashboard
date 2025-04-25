using Microsoft.AspNetCore.Mvc;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View("~/Views/Accounts/Login.cshtml");
        public IActionResult Register() => View("~/Views/Accounts/Register.cshtml");

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ViewBag.Message = "Username already exists.";
                    return View("~/Views/Accounts/Register.cshtml", user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View("~/Views/Accounts/Register.cshtml", user);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid credentials";
            return View("~/Views/Accounts/Login.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
