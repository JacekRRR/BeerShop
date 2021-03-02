using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piwo.Data;
using Piwo.Models;

namespace Piwo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var tags = _db.Tags.ToList();
            return View(tags);
        }
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecialTags tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Add(tag);
                await _db.SaveChangesAsync();
                TempData["Save"] = "Nazwa została dodana";
                return RedirectToAction(nameof(Index));

                
            }
            return View(tag);

        }

        [HttpGet]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfTag = _db.Tags.FirstOrDefault(x => x.Id == id);
            if (typeOfTag == null)
            {
                return NotFound();
            }

            return View(typeOfTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( SpecialTags tag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(tag);
        }

        [HttpGet]

        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var find = _db.Tags.Find(id);

            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Details(Categories category)
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagToDelete = _db.Tags.FirstOrDefault(x => x.Id == id);

            if (tagToDelete == null)
            {
                return NotFound();
            }

            return View(tagToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Delete (int? id, SpecialTags tag)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != tag.Id)
            {
                return NotFound();
            }
            var tagToDelete = _db.Tags.FirstOrDefault(x => x.Id == id);

            if (tagToDelete == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Tags.Remove(tagToDelete);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tagToDelete);
        }
    }
}
