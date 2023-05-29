using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _db;
        public CustomersController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Customer> customers = await _db.Customers.ToListAsync();
            return View(customers);
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
        public async Task<IActionResult> Create(Customer customer)
        {
            bool IsExist=await _db.Customers.AnyAsync(c=>c.Email==customer.Email);
            if (IsExist)
            {
                ModelState.AddModelError("Email", "This Email already exists");
                return View();
            }
            bool isExist = await _db.Customers.AnyAsync(c => c.Mobile == customer.Mobile);
            if (isExist)
            {
                ModelState.AddModelError("Mobile", "This Mobile already exists");
                return View();
            }


            customer.RegistrationDate = DateTime.Now;
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
