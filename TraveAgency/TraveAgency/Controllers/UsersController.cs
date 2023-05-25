using TraveAgency.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace TraveAgency.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        #region Create

        #region get
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {


            IdentityResult identityResult = await _userManager.CreateAsync(appUser, appUser.PasswordHash);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View();
            }
            await _userManager.AddToRoleAsync(appUser, Helper.Roles.Admin.ToString());
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Update

        #region get
        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbAppUser = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAppUser == null)
            {
                return BadRequest();
            }
            return View(dbAppUser);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, AppUser appUser)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbAppUser = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAppUser == null)
            {
                return BadRequest();
            }
            IdentityResult identityResult = await _userManager.UpdateAsync(dbAppUser);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(dbAppUser);
            }

            dbAppUser.Name = appUser.Name;
            dbAppUser.Email = appUser.Email;
            dbAppUser.SurName = appUser.SurName;
            dbAppUser.UserName = appUser.UserName;
            dbAppUser.PasswordHash = appUser.PasswordHash;
            await _userManager.AddToRoleAsync(appUser, Helper.Roles.Admin.ToString());
            await _userManager.UpdateAsync(dbAppUser);
            return RedirectToAction("Index");

        }
        #endregion

        #endregion

        #region Detail
        public async Task<IActionResult> Detail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbAppUser = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAppUser == null)
            {
                return BadRequest();
            }
            return View(dbAppUser);
        }

        #endregion

        #region Activity

        public async Task<IActionResult> Activity(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbAppUser = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAppUser == null)
            {
                return BadRequest();
            }

            if (dbAppUser.IsDeactive)
            {
                dbAppUser.IsDeactive = false;
            }  
            else
            {
                dbAppUser.IsDeactive = true;
            }

            await _userManager.UpdateAsync(dbAppUser);
            return RedirectToAction("Index");
        }

        #endregion

        #region Roles
        public async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Admin.ToString() });
            };
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.ConManager.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.ConManager.ToString() });
            };
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Member.ToString() });
            };
        }
        #endregion

    }
}
