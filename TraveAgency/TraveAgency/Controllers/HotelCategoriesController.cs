using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class HotelCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public HotelCategoriesController(AppDbContext db)
        {
            _db= db;
        }
        public async Task<IActionResult> Index()
        {
            List<HotelCategory> hotelCategories=await _db.HotelCategories.ToListAsync();
            return View(hotelCategories);
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
        public async Task<IActionResult> Create(HotelCategory hotelCategory)
        {
            bool IsExist = await _db.HotelCategories.AnyAsync(t => t.Name == hotelCategory.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View();
            }


            await _db.HotelCategories.AddAsync(hotelCategory);
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
            HotelCategory dbHotelCategory = await _db.HotelCategories.FirstOrDefaultAsync(f => f.Id == id);
            if (dbHotelCategory == null)
            {
                return BadRequest();
            }

            return View(dbHotelCategory);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HotelCategory hotelCategory, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HotelCategory dbHotelCategory = await _db.HotelCategories.FirstOrDefaultAsync(f => f.Id == id);
            if (dbHotelCategory == null)
            {
                return BadRequest();
            }

            bool IsExist = await _db.HotelCategories.AnyAsync(f => f.Name == dbHotelCategory.Name && f.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha Əvvəl istifadə edilib!");
                return View(dbHotelCategory);
            }
            dbHotelCategory.Name = hotelCategory.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #endregion


        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HotelCategory dbHotelCategory = await _db.HotelCategories.FirstOrDefaultAsync(t => t.Id == id);

            if (dbHotelCategory == null)
            {
                return BadRequest();
            }

            if (dbHotelCategory.IsDeactive)
            {
                dbHotelCategory.IsDeactive = false;
            }
            else
            {
                dbHotelCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion


    }
}
