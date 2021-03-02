using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piwo.Data;
using Piwo.Models;

namespace Piwo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var categories = _db.Kategorie.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                _db.Kategorie.Add(category);
                await _db.SaveChangesAsync();
                TempData["save"] = "Kategoria została dodana";
                return RedirectToAction(actionName: nameof(Index));
            }

            return View(category);
        }

        [HttpGet]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TypeOfProduct = _db.Kategorie.Find(id);
            if (TypeOfProduct == null)
            {
                return NotFound();
            }

            return View(TypeOfProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit (Categories category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TypeOfProduct = _db.Kategorie.Find(id);
            if (TypeOfProduct == null)
            {
                return NotFound();
            }

            return View(TypeOfProduct);
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

            var category = _db.Kategorie.FirstOrDefault(x=>x.Id==id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <ActionResult> Delete(int? id, Categories categories)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != categories.Id)
            {
                return NotFound();
            }

            var category = _db.Kategorie.FirstOrDefault(x=>x.Id==id);
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Kategorie.Remove(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categories);
        }
        

        
    }
}
