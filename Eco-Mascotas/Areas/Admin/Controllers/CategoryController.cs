using Eco_Mascotas.Data;
using Eco_Mascotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Eco_Mascotas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.Categories.ToList();
            return View(_db.Categories.ToList());
        }

        //Create Get Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName:nameof(Index));
            }

            return View(category);
        }

        // Get Edit
        public ActionResult Edit(int? id) 
        {
            if (id == null) 
            { 
                return NotFound(); 
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // Post Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);

                await _db.SaveChangesAsync();
                TempData["edit"] = "La categoria ha sido actualizada";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //Details get

        public ActionResult Details(int? id) 
        { 
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //Details post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Category category)
        {
            return RedirectToAction(nameof(Index));
        }

        // Get delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // Post Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Category categories)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != categories.Id)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid) 
            { 
                _db.Remove(category);
                await _db.SaveChangesAsync();
                TempData["delete"] = "La categoria ha sido eliminada";
                return RedirectToAction(nameof(Index));
            }

            return View(categories);
        }
    }
}
