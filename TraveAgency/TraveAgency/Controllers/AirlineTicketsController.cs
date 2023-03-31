using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    public class AirlineTicketsController : Controller
    {
        private readonly AppDbContext _db;
        public AirlineTicketsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<AirlineTicket> tickets = await _db.AirlineTickets
                .Include(t => t.DepartureAirport)
                .Include(t => t.ArrivalAirport)
                .Include(t => t.TransferAirport)
                .Include(t => t.ReturnAirport)
                .Include(t => t.AirlineTicketDetail)
                .Include(t => t.SeatClass)
                .ToListAsync();
            return View(tickets);
        }
    }
}
