using Microsoft.AspNetCore.Mvc;
using MunicipalForms.Models;

namespace MunicipalForms.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (users.Any(u => u.Email == model.Email && u.Password == model.Password))
            {
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (!users.Any(u => u.Email == model.Email))
            {
                users.Add(model);
                return RedirectToAction("Login");
            }

            ViewBag.Error = "User already exists!";
            return View();
        }
    }
}
