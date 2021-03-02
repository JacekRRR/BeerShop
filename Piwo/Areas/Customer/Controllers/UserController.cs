using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piwo.Data;
using Piwo.Models;

namespace Piwo.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _context;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.AppUsers.ToList()) ;
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    var isSaveRole = await _userManager.AddToRoleAsync(user, "User");
                    TempData["save"] = "Dodano użytkownika";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _context.AppUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult>Edit(AppUser user)
        {
            var userToUpdate = _context.AppUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            userToUpdate.UserName = user.UserName;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userToUpdate);

            if (result.Succeeded)
            {
                TempData["save"] = "Dane użytkownika zostały zaktualizowane";
                return RedirectToAction(nameof(Index));

            }
            return View(userToUpdate);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = _context.AppUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Lockout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userToLockout = _context.AppUsers.FirstOrDefault(x => x.Id == id);
            if (userToLockout == null)
            {
                return NotFound();
            }


            return View(userToLockout);
        }
        [HttpPost]

        public async Task<IActionResult> Lockout(AppUser user)
        {
            var userToLockout = _context.AppUsers.FirstOrDefault(x => x.Id == user.Id);
            if (userToLockout == null)
            {
                return NotFound();
            }
            userToLockout.LockoutEnd = DateTime.Now.AddMonths(10);

            var rowAffected = await _context.SaveChangesAsync();
            if (rowAffected > 0)
            {
                TempData["save"] = "Nastąpiła blokada użytkownika";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Active (string id)
        {
            var user = _context.AppUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Active (AppUser user)
        {
            var userInfo = _context.AppUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            int row = await _context.SaveChangesAsync();
            if(row > 0)
            {
               
                
                    TempData["save"] = "Użytkownik został aktywowany";
                    return RedirectToAction(nameof(Index));

                
            }
            return View(userInfo);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _context.AppUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(AppUser user)
        {
            var userInfo = _context.AppUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            _context.Remove(userInfo);
            int row = await _context.SaveChangesAsync();
            if (row > 0)
            {


                TempData["save"] = "Użytkownik został usunięty";
                return RedirectToAction(nameof(Index));


            }
            return View(userInfo);
        }



    }
}
