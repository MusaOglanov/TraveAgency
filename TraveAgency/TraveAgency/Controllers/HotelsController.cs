using AllUp3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    [Authorize]
    public class HotelsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HotelsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #region INDEX
        public async Task<IActionResult> Index(int page=1)
        {
            int showCount = 3;
            ViewBag.PageCount = Math.Ceiling((decimal)await _db.Hotels.CountAsync() / showCount);

            List<Hotel> hotels = await _db.Hotels
                .OrderByDescending(x=>x.Id).Skip((page-1)*showCount).Take(showCount)
                .Include(h => h.HotelDetail)
                .Include(h => h.HotelImages)
                .Include(h => h.HotelRoomTypes)
                .Include(h => h.HotelHotelCategories)
                .ThenInclude(h => h.HotelCategory)
                .ToListAsync();
            ViewBag.CurrentPage = page;
            return View(hotels);
        }
        #endregion

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
        public async Task<IActionResult> Create(Hotel hotel, int[] hotelCatsId, int[] hoteRoomTypesId,
            string checkInTime, string checkOutTime,
            string checkInDate, string checkOutDate)
        {
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();

            #region Images
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
            #endregion

            #region Hotel Category
            List<HotelHotelCategory> hotelHotelCategories = new List<HotelHotelCategory>();

           

            foreach (int hotelCatId in hotelCatsId)
            {
              
                HotelHotelCategory hotelHotelCategory = new HotelHotelCategory
                {

                    HotelCategoryId = hotelCatId,
                };
               
                hotelHotelCategories.Add(hotelHotelCategory);
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

            #endregion

            #region Room Type
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
            #endregion

            #region Check In And Out Time
            string checkInDateTimeStr = $"{checkInDate} {checkInTime}";
            DateTime checkInDateTime = DateTime.Parse(checkInDateTimeStr);

            DateTime currentInTime = DateTime.Now;
            if (checkInDateTime < currentInTime)
            {
                ModelState.AddModelError("HotelDetail.CheckInTime", "Keçmiş tarixlər seçə bilməzsiniz");
                return View();
            }
            hotel.HotelDetail.CheckInTime = checkInDateTime;

            string checkOutDateTimeStr = $"{checkOutDate} {checkOutTime}";
            DateTime checkOutDateTime = DateTime.Parse(checkOutDateTimeStr);
            if (checkInDateTime > checkOutDateTime)
            {
                ModelState.AddModelError("HotelDetail.CheckOutTime", "Check In və Check Out Tarixləri eyni ola bilməz");
                return View();
            }
            DateTime currentOutTime = DateTime.Now;
            if (checkOutDateTime < currentOutTime)
            {
                ModelState.AddModelError("HotelDetail.CheckOutTime", "Keçmiş tarixlər seçə bilməzsiniz");
                return View();
            }
            hotel.HotelDetail.CheckOutTime = checkOutDateTime;
            #endregion

            await _db.Hotels.AddAsync(hotel);
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
            Hotel dbHotel = await _db.Hotels
               .Include(h => h.HotelDetail)
               .Include(h => h.HotelImages)
               .Include(h => h.HotelRoomTypes)
               .Include(h => h.HotelHotelCategories)
               .ThenInclude(h => h.HotelCategory)
               .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.Where(r=>r.HotelId==dbHotel.Id).ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();
            return View(dbHotel);



        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Hotel hotel, int[] hotelCatsId, int[] hoteRoomTypesId, 
            string checkInDate, string checkOutDate,
            string checkInTime, string checkOutTime)
        {
            #region Errors, Include, ViewBag 
            if (id == null)
            {
                return NotFound();
            }
            Hotel dbHotel = await _db.Hotels
               .Include(h => h.HotelDetail)
               .Include(h => h.HotelImages)
               .Include(h => h.HotelRoomTypes)
               .Include(h => h.HotelHotelCategories)
               .ThenInclude(h => h.HotelCategory)
               .FirstOrDefaultAsync(x => x.Id == id);
            if (dbHotel == null)
            {
                return BadRequest();
            }
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();
            #endregion

            #region Images
            List<HotelImage> hotelImages = new List<HotelImage>();
            if (hotel.Photo != null)
            {
                foreach (IFormFile Photo in hotel.Photo)
                {

                    if (!Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Please slect image file");
                        return View();
                    }
                    if (Photo.IsOlder2MB())
                    {
                        ModelState.AddModelError("Photo", "Max 2MB");
                        return View();
                    }
                    string folder = Path.Combine(_env.WebRootPath, "assets", "img");

                    HotelImage hotelImage = new HotelImage
                    {
                        Image = await Photo.SaveImageAsync(folder),
                    };

                    hotelImages.Add(hotelImage);
                }
            }
            dbHotel.HotelImages.AddRange(hotelImages);

            #endregion

            #region Hotel Category And Room Type
            List<HotelHotelCategory> hotelHotelCategories = new List<HotelHotelCategory>();
            foreach (int hotelCatId in hotelCatsId)
            {
                HotelHotelCategory hotelHotelCategory = new HotelHotelCategory
                {
                    HotelCategoryId = hotelCatId,
                };
                hotelHotelCategories.Add(hotelHotelCategory);
            }

            dbHotel.HotelHotelCategories = hotelHotelCategories;
            List<HotelRoomType> hotelRoomTypes = new List<HotelRoomType>();

            foreach (int hoteRoomTypeId in hoteRoomTypesId)
            {
                HotelRoomType hotelRoomType = new HotelRoomType
                {
                    HotelId = hoteRoomTypeId,
                };
                hotelRoomTypes.Add(hotelRoomType);
            }
            dbHotel.HotelHotelCategories = hotelHotelCategories;

            //----------------ROOM TYPE----------

            dbHotel.HotelRoomTypes = hotelRoomTypes;

            if (dbHotel.Star < 1 || dbHotel.Star > 5)
            {
                ModelState.AddModelError("Star", "Please choose a number between 1 and 5 ");
                return View();
            }
            if (dbHotel.HotelDetail.Rating < 1 || dbHotel.HotelDetail.Rating > 10)
            {
                ModelState.AddModelError("HotelDetail.Rating", "Please choose a number between 1 and 10 ");
                return View();
            }
            #endregion

            #region CHECK IN AND OUT TIME
            string checkInDateTimeStr = $"{checkInDate} {checkInTime}";
            DateTime checkInDateTime = DateTime.Parse(checkInDateTimeStr);
            DateTime currentInTime = DateTime.Now;
            if (checkInDateTime < currentInTime)
            {
                ModelState.AddModelError("HotelDetail.CheckInTime", "Keçmiş tarixlər seçə bilməzsiniz");
                return View();
            }
            dbHotel.HotelDetail.CheckInTime = checkInDateTime;

            string checkOutDateTimeStr = $"{checkOutDate} {checkOutTime}";
            DateTime checkOutDateTime = DateTime.Parse(checkOutDateTimeStr);
            if (checkInDateTime < checkOutDateTime)
            {
                ModelState.AddModelError("HotelDetail.CheckOutTime", "Check In və Check Out Tarixləri eyni ola bilməz");
                return View();
            }
            DateTime currentOutTime = DateTime.Now;
            if (checkOutDateTime < currentOutTime)
            {
                ModelState.AddModelError("HotelDetail.CheckOutTime", "Keçmiş tarixlər seçə bilməzsiniz");
                return View();
            }
            dbHotel.HotelDetail.CheckOutTime = checkOutDateTime;
            #endregion


            dbHotel.Name = hotel.Name;
            dbHotel.Price = hotel.Price;
            dbHotel.Country = hotel.Country;
            dbHotel.City = hotel.City;
            dbHotel.Star = hotel.Star;
            dbHotel.HotelDetail.Adress = hotel.HotelDetail.Adress;
            dbHotel.HotelDetail.WebSite = hotel.HotelDetail.WebSite;
            dbHotel.HotelDetail.Email = hotel.HotelDetail.Email;
            dbHotel.HotelDetail.PhoneNumber = hotel.HotelDetail.PhoneNumber;
            dbHotel.HotelDetail.Info = hotel.HotelDetail.Info;
            dbHotel.HotelDetail.RoomAvailable = hotel.HotelDetail.RoomAvailable;
            dbHotel.HotelDetail.Rating = hotel.HotelDetail.Rating;
            dbHotel.HotelDetail.IsDomestic = hotel.HotelDetail.IsDomestic;
          
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
                NotFound();
            }
            Hotel dbHotel = await _db.Hotels
              .Include(h => h.HotelDetail)
              .Include(h => h.HotelImages)
              .Include(h => h.HotelRoomTypes)
              .Include(h => h.HotelHotelCategories)
              .ThenInclude(h => h.HotelCategory)
              .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.HotelRoomType = await _db.HotelRoomTypes.ToListAsync();
            ViewBag.HotelCategory = await _db.HotelCategories.ToListAsync();

            return View(dbHotel);
        }

        #endregion

        #region DeleteImages
        public async Task<IActionResult> DeleteImage(int hotelImageId)
        {
            HotelImage hotelImage = await _db.HotelImages
                .FirstOrDefaultAsync(x => x.Id == hotelImageId);
            string folder = Path.Combine(_env.WebRootPath, "assets", "img");
            Extensions.DeleteFile(folder, hotelImage.Image);
            _db.HotelImages.Remove(hotelImage);
            _db.SaveChanges();
            return Ok();
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hotel dbHotel = await _db.Hotels.FirstOrDefaultAsync(t => t.Id == id);

            if (dbHotel == null)
            {
                return BadRequest();
            }

            if (dbHotel.IsDeactive)
            {
                dbHotel.IsDeactive = false;
            }
            else
            {
                dbHotel.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
