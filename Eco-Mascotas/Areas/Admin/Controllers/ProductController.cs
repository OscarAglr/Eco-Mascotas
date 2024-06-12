using Eco_Mascotas.Data;
using Eco_Mascotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Eco_Mascotas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment he) 
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.Category).Include(f => f.Tag).ToList());
        }

        //POST Index action method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.Category).Include(c => c.Tag)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.Category).Include(c => c.Tag).ToList();
            }
            return View(products);
        }

        //GET Create action method
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            ViewData["TagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");
            return View();
        }

        //POST Create action method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "/Images/" + Path.GetFileName(image.FileName);
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories.ToList(), "Id", "CategoryName");
            ViewData["TagId"] = new SelectList(_db.Tags.ToList(), "Id", "TagName");

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).Include(c => c.Tag)
                .FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        //GET Details Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).Include(c => c.Tag)
                .FirstOrDefault(Tag => Tag.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //GET Delete Action Method
        public ActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).Include(c => c.Tag)
                .Where(c => c.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //POST Delete Action Method
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
