using Eco_Mascotas.Data;
using Eco_Mascotas.Models;
using Eco_Mascotas.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace Eco_Mascotas.Areas.Client.Controllers
{
    [Area("Client")]

    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int? page)
        {
            return View(_db.Products.Include(c => c.Category).Include(c => c.Tag).ToList().ToPagedList(page ?? 1, 9));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET product detail acation method

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Product> Product = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            Product = HttpContext.Session.Get<List<Product>>("Product");
            if (Product == null)
            {
                Product = new List<Product>();
            }
            Product.Add(product);
            HttpContext.Session.Set("Product", Product);
            return RedirectToAction(nameof(Index));
        }
        //GET Remove action methdo
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Product> Product = HttpContext.Session.Get<List<Product>>("Product");
            if (Product != null)
            {
                var product = Product.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    Product.Remove(product);
                    HttpContext.Session.Set("Product", Product);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Product> Product = HttpContext.Session.Get<List<Product>>("Product");
            if (Product != null)
            {
                var product = Product.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    Product.Remove(product);
                    HttpContext.Session.Set("Product", Product);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET product Cart action method

        public IActionResult Cart()
        {
            List<Product> Product = HttpContext.Session.Get<List<Product>>("Product");
            if (Product == null)
            {
                Product = new List<Product>();
            }
            return View(Product);
        }

    }
}
