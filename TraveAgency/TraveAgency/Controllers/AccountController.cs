using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(AppUser user)
        {
            AppUser appUser =await _userManager.FindByNameAsync(user.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("UserName", "Bele username ve ya parol  yoxdur");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("PassworHash", "Profil Bloklanıb");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResault = await _signInManager.PasswordSignInAsync(appUser,user.PasswordHash,user.IsRemember, true);
            if (signInResault.IsLockedOut)
            {
                ModelState.AddModelError("PassworHash", "Profil Bloklanıb for 5 dəq.");
                return View();
            }
            if (!signInResault.Succeeded)
            {
                ModelState.AddModelError("UserName", "Bele username ve ya parol  yoxdur");
                return View();
            }

            return RedirectToAction("Index","Home");
        }
    }
}
