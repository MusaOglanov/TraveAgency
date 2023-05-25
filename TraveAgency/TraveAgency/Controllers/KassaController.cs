using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;
using TraveAgency.ViewModels;

namespace TraveAgency.Controllers
{
    public class KassaController : Controller
    {
        private readonly AppDbContext _db;
        public KassaController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {



            KassaVM kassaVM = new KassaVM
            {
                Income = await _db.Incomes.Include(i=>i.AppUser)
                .OrderByDescending(i => i.CreateTime)
                .FirstOrDefaultAsync(),
                SalaryPaid = await _db.SalaryPaids.Include(i => i.AppUser)
                .OrderByDescending(i => i.CreateTime)
                .FirstOrDefaultAsync(),
                Expense = await _db.Expenses.Include(i => i.AppUser)
                .OrderByDescending(i => i.CreateTime)
                .FirstOrDefaultAsync(),

                Kassa = await _db.Kassa.FirstOrDefaultAsync()

            };

            return View(kassaVM);
        }
    }
}
