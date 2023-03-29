using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
}
