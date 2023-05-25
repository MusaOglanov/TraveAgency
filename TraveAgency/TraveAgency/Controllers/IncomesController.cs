using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _db;
        public IncomesController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Income> incomes = await _db.Incomes.Include(i => i.AppUser).ToListAsync();
            return View(incomes);
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
        public async Task<IActionResult> Create(Income income)
        {
            var kassa = await _db.Kassa.FirstOrDefaultAsync();
            if (kassa == null)
            {
                ModelState.AddModelError("", "kassa tapılmadı");
                return View();
            }
            income.KassaId = kassa.Id;
            kassa.Money += income.Money;
            _db.Kassa.Update(kassa);

            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                ModelState.AddModelError("", "User tapılmadı");
                return View();
            }
            income.AppUserId = user.Id;

            income.CreateTime = DateTime.Now;
            await _db.Incomes.AddAsync(income);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
