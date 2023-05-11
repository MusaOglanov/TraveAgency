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

    public class PaidedSalariesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _db;
        public PaidedSalariesController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<SalaryPaid> salaryPaids = await _db.SalaryPaids
                .Include(s => s.AppUser)
                .Include(s => s.Employee)
                .ToListAsync();
            return View(salaryPaids);
        }

        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.Employee = await _db.Employees.ToListAsync();
            return View();
        }
        #endregion

        #region get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaryPaid salary, int employeesId)
        {
            ViewBag.Employee = await _db.Employees.ToListAsync();

            Kassa kassa = await _db.Kassa.FirstOrDefaultAsync();
            if (kassa == null)
            {
                ModelState.AddModelError("Money", "Kassa Tapılmadı");
                return View();
            }
            salary.KassaId = kassa.Id;
            if (salary.Money > kassa.Money)
            {
                ModelState.AddModelError("Money", "Kassada Yetəri qədər məbləğ yoxdur");
                return View();
            }
            kassa.Money -= salary.Money;
            _db.Kassa.Update(kassa);

            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("Money", "Kassa Tapılmadı");
                return View();
            }
            salary.AppUserId = user.Id;
            salary.CreateTime = DateTime.Now;
            salary.EmployeeId = employeesId;
            await _db.SalaryPaids.AddAsync(salary);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
