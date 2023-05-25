using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    [Authorize]

    public class ExpensesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _db;
        public ExpensesController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Expense> expenses = await _db.Expenses.Include(i => i.AppUser).ToListAsync();
            return View(expenses);
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
        public async Task<IActionResult> Create(Expense expense)
        {
            Kassa kassa = await _db.Kassa.FirstOrDefaultAsync();
            if (kassa == null)
            {
                ModelState.AddModelError("", "Kassa Tapılmadı");
                return View();
            }
            expense.KassaId = kassa.Id;
            if (expense.Money > kassa.Money)
            {
                ModelState.AddModelError("Money", "Kassada Yetəri qədər məbləğ yoxdur");
                return View();
            }
            kassa.Money -= expense.Money;
            _db.Kassa.Update(kassa);

            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "User Tapılmadı");
                return View();
            }
            expense.AppUserId=user.Id;
            expense.CreateTime= DateTime.Now;

            await _db.Expenses.AddAsync(expense);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
