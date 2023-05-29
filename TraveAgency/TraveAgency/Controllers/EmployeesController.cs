using Microsoft.AspNetCore.Authorization;
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
    [Authorize]

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

             bool isExist = await _db.Employees.AnyAsync(e => e.Email == employee.Email);
            if(isExist)
            {
                ModelState.AddModelError("Email", "This Email already exists");
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

        #region Update
        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            Employee dbEmployee =await _db.Employees.Include(e=>e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if(dbEmployee == null)
            {
                return BadRequest();
            }
            ViewBag.Positions = await _db.Positions.ToListAsync();

            return View(dbEmployee);
        }
        #endregion

        #region get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Employee employee, int positionId, DateTime birthDate, DateTime hireDate)
        {
            if(id== null)
            {
                return NotFound();
            }
            Employee dbEmployee =await _db.Employees.Include(e=>e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if(dbEmployee == null)
            {
                return BadRequest();
            }
            ViewBag.Positions = await _db.Positions.ToListAsync();
            dbEmployee.PositionId = positionId;

            bool Isexist = await _db.Employees.AnyAsync(e => e.Email == employee.Email && e.Id != id);
            if (Isexist)
            {
                ModelState.AddModelError("Email", "This email already exists");
                return View(dbEmployee);
            }
            dbEmployee.FullName = employee.FullName;
            dbEmployee.Email = employee.Email;
            dbEmployee.Mobile = employee.Mobile;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.Notes = employee.Notes;
            dbEmployee.Bithdate = birthDate;
            dbEmployee.HireDate = hireDate;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee dbEmployee = await _db.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (dbEmployee == null)
            {
                return BadRequest();
            }
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View(dbEmployee);
        }


        #endregion

        #region Status
        public async Task<IActionResult> Status(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee dbEmployee = await _db.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);

            if (dbEmployee == null)
            {
                return BadRequest();
            }

            if (dbEmployee.Status)
            {
                dbEmployee.Status = false;
            }
            else
            {
                dbEmployee.Status = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
