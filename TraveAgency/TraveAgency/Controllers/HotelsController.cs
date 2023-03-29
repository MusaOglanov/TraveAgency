using AllUp3.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class HotelsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HotelsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Hotel> hotels = await _db.Hotels
                .Include(h=>h.HotelDetail)
                .Include(h=>h.HotelImages)
                .Include(h=>h.HotelRoomTypes)
                .Include(h=>h.HotelHotelCategories)
                .ThenInclude(h=>h.HotelCategory)
                .ToListAsync();
            return View(hotels);
        }


        #region Create

        #region Get
        public async Task<IActionResult> Create()
        {
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();

            return View();
        }
        #endregion



        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel, int[] hotelCatsId, int[] hoteRoomTypesId)
        {
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();
            List<HotelImage> hotelImages = new List<HotelImage>();
            if (hotel.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please slect image ");
                return View();
            }
            foreach (IFormFile photo in hotel.Photo)
            {

                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please slect image file");
                    return View();
                }
                if (photo.IsOlder2MB())
                {
                    ModelState.AddModelError("Photo", "Max 2MB");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "img");
                HotelImage hotelImage = new HotelImage
                {
                    Image = await photo.SaveImageAsync(folder),
                };

                hotelImages.Add(hotelImage);
            }
            hotel.HotelImages = hotelImages;
            
            List<HotelHotelCategory> hotelHotelCategories = new List<HotelHotelCategory>();
            foreach (int  hotelCatId in hotelCatsId)
            {
                HotelHotelCategory hotelHotelCategory = new HotelHotelCategory
                {
                    HotelCategoryId = hotelCatId,
                };
                hotelHotelCategories.Add(hotelHotelCategory);
            }
            if (hotel.HotelDetail.IsDomestic == true)
            {
            }
            hotel.HotelHotelCategories = hotelHotelCategories;
            List<HotelRoomType> hotelRoomTypes = new List<HotelRoomType>();
            
            foreach (int hoteRoomTypeId in hoteRoomTypesId)
            {
                HotelRoomType hotelRoomType = new HotelRoomType
                {
                    HotelId = hoteRoomTypeId,
                };
                hotelRoomTypes.Add(hotelRoomType);
            }
            
            
            hotel.HotelRoomTypes = hotelRoomTypes;
            if (hotel.Star < 1 || hotel.Star > 5)
            {
                ModelState.AddModelError("Star", "Please choose a number between 1 and 5 ");
                return View();
            }
            if (hotel.HotelDetail.Rating < 1 || hotel.HotelDetail.Rating > 10)
            {
                ModelState.AddModelError("HotelDetail.Rating", "Please choose a number between 1 and 10 ");
                return View();
            }
            



            await _db.Hotels.AddAsync(hotel);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion

        #endregion





    }
}
