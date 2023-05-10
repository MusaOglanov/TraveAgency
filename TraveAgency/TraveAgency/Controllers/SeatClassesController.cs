using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    [Authorize]
    public class SeatClassesController : Controller
    {
        private readonly AppDbContext _db;
        public SeatClassesController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<SeatClass> seatClasses = await _db.SeatClasses.ToListAsync();
            return View(seatClasses);

        }
        #endregion

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
        public async Task<IActionResult> Create(SeatClass seatClass)
        {
            bool IsExist = await _db.SeatClasses.AnyAsync(s => s.Name == seatClass.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", " artıq eyni adlı sinif var");
                return View();
            }

            await _db.SeatClasses.AddAsync(seatClass);
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
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }

            return View(dbSeatClass);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SeatClass seatClass)
        {
            if (id == null)
            {
                return NotFound();
            }
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }
            bool IsExist = await _db.SeatClasses.AnyAsync(s => s.Name == seatClass.Name && s.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", " artıq eyni adlı sinif var");
                return View();
            }
            dbSeatClass.Name = seatClass.Name;
            dbSeatClass.SeatPrice = seatClass.SeatPrice;
            dbSeatClass.Info = seatClass.Info;
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
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }
            return View(dbSeatClass);
        }

        #endregion


        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }
            if (dbSeatClass.IsDeactive)
            {
                dbSeatClass.IsDeactive = false;
            }
            else
            {
                dbSeatClass.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            #endregion



        }


    }
}
