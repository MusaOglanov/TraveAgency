using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using System.Threading.Tasks;
using TraveAgency.Models;
using TraveAgency.ViewModels;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM loginVM)
        {
            AppUser appUser =await _userManager.FindByNameAsync(loginVM.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Bele username ve ya parol  yoxdur");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "Profil Bloklanıb");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResault = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password,loginVM.IsRemember,true);
            if (signInResault.IsLockedOut)
            {
                ModelState.AddModelError("", "Profil Bloklanıb 5 dəq.");
                return View();
            }
            if (!signInResault.Succeeded)
            {
                ModelState.AddModelError("UserName", "Bele username ve ya parol  yoxdur");
                return View();
            }
            return RedirectToAction("Index","Home");
        }

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        #endregion
    }
}
