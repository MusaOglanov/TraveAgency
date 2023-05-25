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
            SignInManager<AppUser> signInManager )

        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region LogIn

        #region get
        public IActionResult LogIn()
        {
            return View();
        }
        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM loginVM)
        {


            AppUser appUser = await _userManager.FindByNameAsync(loginVM.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "The user is blocked");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResault = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.IsRemember, true);
            if (signInResault.IsLockedOut)
            {
                ModelState.AddModelError("", "Profile blocked for 5 minutes");
                return View();
            }
            if (!signInResault.Succeeded)
            {
                ModelState.AddModelError("UserName", "Username or Password is incorrect");
                return View();
            }


            return RedirectToAction("Index", "Home");
            #endregion

            #endregion


        }

    }
}
