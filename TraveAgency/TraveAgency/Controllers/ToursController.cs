using AllUp3.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class ToursController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ToursController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Tour> tours = await _db.Tours
                .Include(t => t.TourCategory)
                .Include(t => t.TourHotels)
                .ThenInclude(t => t.Hotel)
                .ToListAsync();
            return View(tours);
        }

        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.TourCategory = await _db.TourCategories.ToListAsync();
            ViewBag.TourHotel = await _db.Hotels.ToListAsync();
            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tour tour, int tourCatsId, int[] hotelsId, DateTime startDate, DateTime endDate)
        {
            ViewBag.TourCategory = await _db.TourCategories.ToListAsync();
            ViewBag.TourHotel = await _db.Hotels.ToListAsync();
            bool IsExist = await _db.Tours.AnyAsync(t => t.Name == tour.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha əvvəl istifadə olunub");
                return View();
            }
            if (tour.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa Şəkil seçin");
                return View();
            }
            if (!tour.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa Şəkil faylı seçin");
                return View();
            }
            if (tour.Photo.IsOlder2MB())
            {
                ModelState.AddModelError("Photo", "Seçilə biləcək maxsimum şəkil ölçüsü 2MB-dır");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "assets", "img");
            tour.Image = await tour.Photo.SaveImageAsync(folder);
            List<TourHotel> tourHotels = new List<TourHotel>();
            foreach (int tourHotelId in hotelsId)
            {
                TourHotel tourHotel = new TourHotel
                {
                    HotelId = tourHotelId
                };
                tourHotels.Add(tourHotel);
            }
            tour.TourHotels = tourHotels;
            tour.StartDate = startDate;
            tour.EndDate = endDate;
            tour.TourCategoryId = tourCatsId;
            await _db.Tours.AddAsync(tour);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region MyRegion

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tour dbTour = await _db.Tours
                .Include(t => t.TourCategory)
                .Include(t => t.TourHotels)
                .ThenInclude(t => t.Hotel)
                .FirstOrDefaultAsync(t => t.Id == id);
            ViewBag.TourCategory = await _db.TourCategories.ToListAsync();
            ViewBag.TourHotel = await _db.Hotels.ToListAsync();
            return View(dbTour);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Tour tour, int tourCatsId, int[] hotelsId, DateTime startDate, DateTime endDate)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tour dbTour = await _db.Tours
                .Include(t => t.TourCategory)
                .Include(t => t.TourHotels)
                .ThenInclude(t => t.Hotel)
                .FirstOrDefaultAsync(t => t.Id == id);
            ViewBag.TourCategory = await _db.TourCategories.ToListAsync();
            ViewBag.TourHotel = await _db.Hotels.ToListAsync();

            bool IsExist = await _db.Tours.AnyAsync(t => t.Name == tour.Name&&t.Id!=id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha əvvəl istifadə olunub");
                return View();
            }
            if (tour.Photo != null)
            {
                if (!tour.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zəhmət olmasa Şəkil faylı seçin");
                    return View();
                }
                if (tour.Photo.IsOlder2MB())
                {
                    ModelState.AddModelError("Photo", "Seçilə biləcək maxsimum şəkil ölçüsü 2MB-dır");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "img");
                dbTour.Image = await tour.Photo.SaveImageAsync(folder);
            }
           

            List<TourHotel> tourHotels = new List<TourHotel>();
            foreach (int tourHotelId in hotelsId)
            {
                TourHotel tourHotel = new TourHotel
                {
                    HotelId = tourHotelId
                };
                tourHotels.Add(tourHotel);
            }
            dbTour.TourHotels = tourHotels;



            dbTour.Name = tour.Name;
            dbTour.Country = tour.Country;
            dbTour.City = tour.City;
            dbTour.TourPrice = tour.TourPrice;
            dbTour.Adults = tour.Adults;
            dbTour.Children = tour.Children;
            dbTour.TourDescription = tour.TourDescription;
            dbTour.Duration = tour.Duration;
            dbTour.IsDomestic = tour.IsDomestic;
            dbTour.StartDate = tour.StartDate;
            dbTour.EndDate = tour.EndDate;
            dbTour.TourCategoryId =tourCatsId;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

    }
}
