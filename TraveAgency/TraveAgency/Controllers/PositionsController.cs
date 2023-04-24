using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class PositionsController : Controller
    {
        private readonly AppDbContext _db;
        public PositionsController(AppDbContext db)
        {
            _db=db;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
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
        public async Task<IActionResult> Create(Position position)
        {
            bool IsExist=await _db.Positions.AnyAsync(p=>p.Name==position.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha əvvəl istifadə edilib");
                return View();
            }
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
