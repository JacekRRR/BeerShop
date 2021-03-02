using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Piwo.Data;
using Piwo.Models;

namespace Piwo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public ProductController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
           
            return View(_context.Piwa.Include(x => x.Category).Include(c => c.specialTags).ToList());
        }

        [HttpPost]

        public IActionResult Index(decimal? lowPrice, decimal? highPrice)
        {
            var products = _context.Piwa.Include(x => x.Category).Include(y => y.specialTags)
                           .Where(c => c.Price >= lowPrice && c.Price <= highPrice).ToList();

            if (lowPrice == null || highPrice == null)
            {
                products= _context.Piwa.Include(x => x.Category).Include(y => y.specialTags)
                           .ToList();
            }

            return View(products);
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            ViewBag.CategoriesId = new SelectList(_context.Kategorie.ToList(), "Id", "TypeOfProduct");
            ViewBag.SpecialTagsId = new SelectList(_context.Tags.ToList(), "Id", "TagName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produkty produkt, IFormFile image)
        {

            ViewBag.Message = "Ten przedmiot znajduje się już w bazie";
            ViewBag.CategoriesId = new SelectList(_context.Kategorie.ToList(), "Id", "TypeOfProduct");
            ViewBag.SpecialTagsId = new SelectList(_context.Tags.ToList(), "Id", "TagName");
            if (ModelState.IsValid)
            {
                var searchItem = _context.Piwa.FirstOrDefault(x => x.Name == produkt.Name);
                if (searchItem != null)
                {

                    return View(searchItem);
                }



                if (image != null)
                {
                    var name = Path.Combine(_env.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name,FileMode.Create));
                    produkt.Image = image.FileName;
                }

                if (image == null)
                {
                    produkt.Image = "Images/NoImage.jpg";
                }
                _context.Piwa.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.CategoriesId = new SelectList(_context.Kategorie.ToList(), "Id", "TypeOfProduct");
            ViewBag.SpecialTagsId = new SelectList(_context.Tags.ToList(), "Id", "TagName");
            if (id == null)
            {
                return NotFound();
            }
            
            var ItemToEdit = _context.Piwa.Include(c=>c.Category).Include(x=>x.specialTags).FirstOrDefault(x => x.Id == id);

            if (ItemToEdit == null)
            {
                return NotFound();
            }
            
            return View(ItemToEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produkty produkt, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_env.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    produkt.Image = image.FileName;
                }

                if (image == null)
                {
                    produkt.Image = "Images/NoImage.jpg";
                }
                _context.Piwa.Update(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

       [HttpGet]
       
       public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Piwa.Include(y=>y.Category).Include(z=>z.specialTags).FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = _context.Piwa.Include(x => x.Category).Include(z => z.specialTags).FirstOrDefault(n => n.Id == id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            return View(itemToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DConfirmation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToDelete = _context.Piwa.FirstOrDefault(x => x.Id == id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Piwa.Remove(productToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}
