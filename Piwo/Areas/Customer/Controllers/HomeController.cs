using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piwo.Data;
using Piwo.Models;
using Piwo.Utylity;
using X.PagedList;

namespace Piwo.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            return View(_context.Piwa.Include(x=>x.Category).Include(y=>y.specialTags).ToList().ToPagedList(page??1,3));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Piwa.Include(x => x.Category).Include(y => y.specialTags).FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        [HttpPost]
        [ActionName("Details")]
        
        public IActionResult AddToBascet(int? id)
        {
            List<Produkty> products = new List<Produkty>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Piwa.Include(x => x.Category).FirstOrDefault(t => t.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Produkty>>("products");
            if (products == null)
            {
                products = new List<Produkty>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);

            return View(product);

        }

        [ActionName("Remove")]
        [HttpGet]
        public IActionResult RemoveToCard(int? id)
        {
            List<Produkty> products = HttpContext.Session.Get<List<Produkty>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(y => y.Id == id);

                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Produkty> products = HttpContext.Session.Get<List<Produkty>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(y => y.Id == id);

                if(product!=null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public IActionResult Cart()
        {
            List<Produkty> products = HttpContext.Session.Get<List<Produkty>>("products");
            if (products == null)
            {
                products = new List<Produkty>();
            }
            return View(products);
        }

        
        
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
    }
}
