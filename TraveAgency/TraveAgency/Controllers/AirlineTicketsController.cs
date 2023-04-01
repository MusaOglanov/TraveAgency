using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                   .Include(t => t.ReturnAirport)
                   .Include(t => t.TransferAirport)
                   .Include(t => t.AirlineTicketDetail)
                   .Include(t => t.SeatClass)
                   .ToListAsync();
            return View(tickets);
        }


        #region Create
        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();

            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            AirlineTicket ticket,
            int depAirId, int arrAirId,
            int reAirId, int transAirId, int seatClassId, int transferPriceId, int? returnPriceId,int baggageId,
            string departureDate, string departureTime,
            string flightDuration, string arrivalDate, string arrivalTime,
            string transferTime, string transferDate, string returnDate, string returnTime)


        {
            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();

            if (arrAirId == depAirId)
            {
                ModelState.AddModelError("ArrivalAirport", "Gediş və Eniş Aeroportları eyni ola bilməz");
                return View();
            }

            if (ticket.AirlineTicketDetail.IsTransfer)
            {
                if (transAirId==0)
                {
                    ModelState.AddModelError("AirlineTicketDetail.TransferPrice", "Lutfen qiym'ti daxil edin");
                    return View();
                }
                if (transAirId == depAirId || transAirId == arrAirId)
                {
                    ModelState.AddModelError("TransferAirport", "Aeroportlar eyni ola bilməz");
                    return View();
                }

                string transferDateTimeStr = $"{transferDate} {transferTime}";
                DateTime trasferDateTime = DateTime.Parse(transferDateTimeStr);
                ticket.AirlineTicketDetail.TransferTime = trasferDateTime;
                ticket.AirlineTicketDetail.TransferPrice = transferPriceId;
                ticket.TransferAirportId = transAirId;

            }

            if (ticket.AirlineTicketDetail.IsReturn)
            {

                if (returnPriceId == 0)
                {
                    ModelState.AddModelError("AirlineTicketDetail.ReturnPrice", "Qiyməti daxil edin");
                    return View();
                }

                if (reAirId == depAirId || reAirId == transAirId)
                {
                    ModelState.AddModelError("TransferAirport", "Aeroportlar eyni ola bilməz");
                    return View();
                }
                ticket.AirlineTicketDetail.ReturnPrice = returnPriceId;
                string returnDateTimeStr = $"{returnDate} {returnTime}";
                DateTime returnDateTime = DateTime.Parse(returnDateTimeStr);
                ticket.AirlineTicketDetail.ReturnTime = returnDateTime;
                ticket.ReturnAirportId = reAirId;
            }

            if (ticket.AirlineTicketDetail.HassBaggage)
            {
                if (baggageId == 0)
                {
                    ModelState.AddModelError("AirlineTicketDetail.BaggagePrice", "Lutfen qiym'ti daxil edin");
                    return View();
                }
                
                ticket.AirlineTicketDetail.BaggagePrice = baggageId;

            }

            string departureDateTimeStr = $"{departureDate} {departureTime}";
            string arrivalDateTimeStr = $"{arrivalDate} {arrivalTime}";


            DateTime departureDateTime = DateTime.Parse(departureDateTimeStr);
            ticket.DepartureDateTime = departureDateTime;

            DateTime arrivalDateTime = DateTime.Parse(arrivalDateTimeStr);
            ticket.ArrivalDateTime = arrivalDateTime;

            TimeSpan flightDurationTime = TimeSpan.Parse(flightDuration);
            ticket.FlightDuration = flightDurationTime;



            ticket.DepartureAirportId = depAirId;
            ticket.ArrivalAirportId = arrAirId;
            ticket.SeatClassId = seatClassId;

            await _db.AirlineTickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion
    }
}
