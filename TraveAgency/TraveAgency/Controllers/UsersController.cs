using TraveAgency.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;
using System.Runtime.InteropServices;

namespace TraveAgency.Controllers
{
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


           IdentityResult identityResult= await _userManager.CreateAsync(appUser,appUser.PasswordHash);
            if(!identityResult.Succeeded)
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
