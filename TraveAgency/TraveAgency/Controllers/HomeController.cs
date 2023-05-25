using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;
using TraveAgency.ViewModels;

namespace TraveAgency.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(AppDbContext db, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
          
            HomeVM homeVM = new HomeVM
            {
                Tour = await _db.Tours.FirstOrDefaultAsync(),

                Hotel = await _db.Hotels
               .Include(h => h.HotelDetail)
                .Include(h => h.HotelImages).FirstOrDefaultAsync(),

                AirlineTicket = await _db.AirlineTickets
                   .Include(t => t.DepartureAirport)
                   .FirstOrDefaultAsync(),

            };
            return View(homeVM);
        }
        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

        #endregion

        public IActionResult Error()
        {
            return View();
        }
    }
}
