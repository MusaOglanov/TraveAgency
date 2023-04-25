using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _db.Employees.Include(e=>e.Position).ToListAsync();

            return View(employees);
        }

        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee,int positionId,DateTime birthDate,DateTime hireDate)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            bool IsExist = await _db.Employees.AnyAsync(e => e.FullName == employee.FullName);
            if(IsExist)
            {
                ModelState.AddModelError("FullName", "Eyni adda işçi artıq mövcuddur");
                return View();
            }
             bool Isexist = await _db.Employees.AnyAsync(e => e.Email == employee.Email);
            if(Isexist)
            {
                ModelState.AddModelError("Email", "Bu email artıq mövcuddur");
                return View();
            }


            employee.Bithdate = birthDate;
            employee.HireDate = hireDate;
            employee.PositionId = positionId;
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
