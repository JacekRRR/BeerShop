using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Piwo.Areas.Admin.Models;
using Piwo.Data;

namespace Piwo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _manager;
        ApplicationDbContext _context;

        public RoleController(RoleManager<IdentityRole> manager,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _manager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _manager.RoleExistsAsync(role.Name);
            if(isExist)
            {
                ViewBag.Msg = "Dodawana rola już istnieje";
                ViewBag.Name = name;
                return View();
            }
            var result = await _manager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Rola została dodana";
                RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit (string id)
        {
            var roleToEdit = await _manager.FindByIdAsync(id);
            if (roleToEdit == null)
            {
                return NotFound();
            }
            ViewBag.Id = roleToEdit.Id;
            ViewBag.Name = roleToEdit.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var role = await _manager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;

            var isExist = await _manager.RoleExistsAsync(role.Name);
            if(isExist)
            {
                ViewBag.Msg = "Ta rola już istnieje";
                ViewBag.Name = name;
                return View();
            }
           
            var result = await _manager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Rola została zaktualizowana";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _manager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            ViewBag.Id = role.Id;
            ViewBag.Name = role.Name;
            return View();

        }

        [HttpPost]

        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost (string id)
        {
            var roleToDelete = await _manager.FindByIdAsync(id);

            if (roleToDelete == null)
            {
                return NotFound();
            }

            var result = await _manager.DeleteAsync(roleToDelete);
            if (result.Succeeded)
            {
                TempData["save"] = "Rola została usunięta";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Assign()
        {
            ViewBag.UserId = new SelectList(_context.AppUsers.Where(f=>f.LockoutEnd<DateTime.Now||f.LockoutEnd==null).ToList(),"Id","UserName");
            ViewBag.RoleId = new SelectList(_manager.Roles.ToList(), "Name","Name");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Assign(RoleUserVm ruVm)
        {
            var user = _context.AppUsers.FirstOrDefault(c => c.Id == ruVm.UserId);

            var isCheckRoleAssign = await _userManager.IsInRoleAsync(user, ruVm.RoleId);
            if (isCheckRoleAssign)
            {
                ViewBag.mgs = "Ten użytkownik ma już przypisaną rolę.";
                ViewBag.UserId = new SelectList(_context.AppUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewBag.RoleId = new SelectList(_manager.Roles.ToList(), "Name", "Name");
                return View();
            }

            

            var role = await _userManager.AddToRoleAsync(user, ruVm.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "Rola została przypisana użytkownikowi";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public ActionResult AssignUserRole()
        {
            var result = from ur in _context.UserRoles
                         join r in _context.Roles on ur.RoleId equals r.Id
                         join a in _context.AppUsers on ur.UserId equals a.Id
                         select new UserRoleMaping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name

                         };
            ViewBag.UserRoles = result;
            return View();
        }
    }
}
