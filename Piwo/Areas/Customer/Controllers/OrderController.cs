using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piwo.Data;
using Piwo.Models;
using Piwo.Utylity;

namespace Piwo.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult CheckOut()
        {
            return View();
                
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public async Task<IActionResult> CheckOut(Order anOrder)
        {
            List<Produkty> products = HttpContext.Session.Get<List<Produkty>>("products");
            if (products != null)
            {
                foreach(var product in products)
                {
                    OrdedDetails ordedDetails = new OrdedDetails();
                    ordedDetails.ProductId = product.Id;
                   
                    anOrder.OrdersDetails.Add(ordedDetails);
                }
            }
            anOrder.OrderNumber = GetOrderNumber();
            _context.Orders.Add(anOrder);
            await _context.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Produkty>());
            return View();
        }

        public string GetOrderNumber()
        {
            int orderCount = _context.Orders.ToList().Count()+1;
            return orderCount.ToString("000");
        }
        
    }
    
}
