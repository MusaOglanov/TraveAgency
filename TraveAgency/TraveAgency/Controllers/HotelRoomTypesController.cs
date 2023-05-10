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
    public class HotelRoomTypesController : Controller
    {
        private readonly AppDbContext _db;

        public HotelRoomTypesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<HotelRoomType> hotelRoomTypes = await _db.HotelRoomTypes.ToListAsync();
            return View(hotelRoomTypes);
        }


        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.Hotel= await _db.Hotels.ToListAsync();
            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelRoomType hotelRoomType,int hotelId)
        {
            ViewBag.Hotel = await _db.Hotels.ToListAsync();

          
            hotelRoomType.HotelId=hotelId;
            await _db.HotelRoomTypes.AddAsync(hotelRoomType);
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
            HotelRoomType dbHotelRoomType = await _db.HotelRoomTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (dbHotelRoomType == null)
            {
                return BadRequest();
            }

            ViewBag.Hotel = await _db.Hotels.ToListAsync();

            return View(dbHotelRoomType);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HotelRoomType hotelRoomType, int? id,int hotelId)
        {
            if (id == null)
            {
                return NotFound();
            }
            HotelRoomType dbHotelRoomType = await _db.HotelRoomTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (dbHotelRoomType == null)
            {
                return BadRequest();
            }

            bool IsExist = await _db.HotelRoomTypes.AnyAsync(f => f.Name == dbHotelRoomType.Name && f.Id != id);
            if (IsExist)

            ViewBag.Hotel = await _db.Hotels.ToListAsync();

            dbHotelRoomType.Name = hotelRoomType.Name;
            dbHotelRoomType.HotelId = hotelId;
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
            HotelRoomType dbHotelRoomType = await _db.HotelRoomTypes.FirstOrDefaultAsync(h => h.Id == id);
            if (dbHotelRoomType == null)
            {
                return BadRequest();
            }
            ViewBag.Hotel = await _db.Hotels.ToListAsync();

            return View(dbHotelRoomType);
        }


        #endregion




        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HotelRoomType dbHotelRoomType = await _db.HotelRoomTypes.FirstOrDefaultAsync(t => t.Id == id);

            if (dbHotelRoomType == null)
            {
                return BadRequest();
            }

            if (dbHotelRoomType.IsDeactive)
            {
                dbHotelRoomType.IsDeactive = false;
            }
            else
            {
                dbHotelRoomType.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
