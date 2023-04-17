using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class TourCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public TourCategoriesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<TourCategory> categories = await _db.TourCategories.ToListAsync() ;
            return View(categories);
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
        public async Task<IActionResult> Create(TourCategory category)
        {
            bool IsExist =await _db.TourCategories.AnyAsync(c => c.Name == category.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha əvvəl istifadə edilib");
                return View();
            }
            await _db.TourCategories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Update

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            TourCategory dbCategory=await _db.TourCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }
            return View(dbCategory);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,TourCategory category)
        {
            if (id == null)
            {
                return NotFound();
            }
            TourCategory dbCategory = await _db.TourCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }
            bool IsExist = await _db.TourCategories.AnyAsync(c => c.Name == category.Name&&c.Id!=id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha əvvəl istifadə edilib");
                return View();
            }
            dbCategory.Name = category.Name;
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
            TourCategory dbCategory = await _db.TourCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }
            if (dbCategory.IsDeactive)
            {
                dbCategory.IsDeactive = false;
            }
            else
            {
                dbCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            #endregion
        }
    }
}
