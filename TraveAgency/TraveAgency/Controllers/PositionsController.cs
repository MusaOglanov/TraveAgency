using Microsoft.AspNetCore.Authorization;
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
    public class PositionsController : Controller
    {
        private readonly AppDbContext _db;
        public PositionsController(AppDbContext db)
        {
            _db = db;
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
            bool IsExist = await _db.Positions.AnyAsync(p => p.Name == position.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name has been used before");
                return View();
            }
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion 

        #region Update

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Position dbPosition = await _db.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (dbPosition == null)
            {
                return BadRequest();
            }
            return View(dbPosition);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Position position, int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Position dbPosition = await _db.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (dbPosition == null)
            {
                return BadRequest();
            }
            bool IsExist = await _db.Positions.AnyAsync(p => p.Name == position.Name && p.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name has been used before");
                return View();
            }

            dbPosition.Name = position.Name;
            dbPosition.Description = position.Description;
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
            Position dbPosition = await _db.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (dbPosition == null)
            {
                return BadRequest();
            }
            return View(dbPosition);
        }

        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Position dbPosition = await _db.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (dbPosition == null)
            {
                return BadRequest();
            }
            if (dbPosition.IsDeactive)
            {
                dbPosition.IsDeactive = false;
            }
            else
            {
                dbPosition.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            #endregion
        }
    }
}
