using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Controllers
{
    public class ThemeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CurrentTheme = Request.Cookies["UserTheme"] ?? "White";
            return View();
        }

        [HttpPost]
        public IActionResult SaveTheme(string theme)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("UserTheme", theme, options);

            return RedirectToAction(nameof(Index));
        }
    }
}