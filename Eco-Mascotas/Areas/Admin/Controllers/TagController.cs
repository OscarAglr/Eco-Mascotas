using Eco_Mascotas.Data;
using Eco_Mascotas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eco_Mascotas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private ApplicationDbContext _db;
        public TagController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.Categories.ToList();
            return View(_db.Tags.ToList());
        }

        //Create Get Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Add(tag);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }

            return View(tag);
        }
        
        // Get Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _db.Tags.Find(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // Post Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tag);

                await _db.SaveChangesAsync();
                TempData["edit"] = "La etiqueta ha sido actualizada";
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        //Details get

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _db.Tags.Find(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        //Details post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Tag tag)
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

            var tag = _db.Tags.Find(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // Post Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Tag tags)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != tags.Id)
            {
                return NotFound();
            }

            var tag = _db.Tags.Find(id);

            if (tag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(tag);
                await _db.SaveChangesAsync();
                TempData["delete"] = "La etiqueta ha sido eliminada";
                return RedirectToAction(nameof(Index));
            }

            return View(tags);
        }
    }
}
