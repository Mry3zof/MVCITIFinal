using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Models;

namespace ProjectName.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> ProductsList = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 25000 },
            new Product { Id = 2, Name = "Keyboard", Price = 1500 },
            new Product { Id = 3, Name = "Mouse", Price = 800 }
        };

        public IActionResult Index()
        {
            ViewBag.CurrentTheme = Request.Cookies["UserTheme"] ?? "White";
            return View(ProductsList);
        }

        public IActionResult AddToSession(int id)
        {
            var product = ProductsList.Find(p => p.Id == id);
            if (product != null)
            {
                HttpContext.Session.SetString($"product-details-{id}", $"{product.Name} - ${product.Price}");
            }
            return RedirectToAction("Details", new { id });
        }

        public IActionResult Details(int id)
        {
            ViewBag.ProductInfo = HttpContext.Session.GetString($"product-details-{id}") ?? "No session data found";
            return View(ProductsList.Find(p => p.Id == id));
        }
    }
}