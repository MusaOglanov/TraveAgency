﻿using Microsoft.AspNetCore.Mvc;
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


        #region Update

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            AirlineTicket dbTickets = await _db.AirlineTickets
                  .Include(t => t.DepartureAirport)
                  .Include(t => t.ArrivalAirport)
                  .Include(t => t.ReturnAirport)
                  .Include(t => t.TransferAirport)
                  .Include(t => t.AirlineTicketDetail)
                  .Include(t => t.SeatClass)
                  .FirstOrDefaultAsync(x=>x.Id==id);

            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();

            return View(dbTickets);

        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, AirlineTicket ticket,
            int depAirId, int arrAirId,
            int reAirId, int transAirId, int seatClassId, int transferPriceId, int? returnPriceId, int baggageId,
            string departureDate, string departureTime,
            string flightDuration, string arrivalDate, string arrivalTime,
            string transferTime, string transferDate, string returnDate, string returnTime)
        {
            if (id == null)
            {
                return NotFound();
            }


            AirlineTicket dbTicket = await _db.AirlineTickets
                  .Include(t => t.DepartureAirport)
                  .Include(t => t.ArrivalAirport)
                  .Include(t => t.ReturnAirport)
                  .Include(t => t.TransferAirport)
                  .Include(t => t.AirlineTicketDetail)
                  .Include(t => t.SeatClass)
                  .FirstOrDefaultAsync(x => x.Id == id);
            if(dbTicket == null)
            {
                return BadRequest();
            }

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
                if (transAirId == 0)
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
                dbTicket.AirlineTicketDetail.TransferTime = trasferDateTime;
                dbTicket.AirlineTicketDetail.TransferPrice = transferPriceId;
                dbTicket.TransferAirportId = transAirId;

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
                dbTicket.AirlineTicketDetail.ReturnPrice = returnPriceId;
                string returnDateTimeStr = $"{returnDate} {returnTime}";
                DateTime returnDateTime = DateTime.Parse(returnDateTimeStr);
                dbTicket.AirlineTicketDetail.ReturnTime = returnDateTime;
                dbTicket.ReturnAirportId = reAirId;
            }

            if (ticket.AirlineTicketDetail.HassBaggage)
            {
                if (baggageId == 0)
                {
                    ModelState.AddModelError("AirlineTicketDetail.BaggagePrice", "Lutfen qiym'ti daxil edin");
                    return View();
                }

                dbTicket.AirlineTicketDetail.BaggagePrice = baggageId;

            }

            string departureDateTimeStr = $"{departureDate} {departureTime}";
            string arrivalDateTimeStr = $"{arrivalDate} {arrivalTime}";


            DateTime departureDateTime = DateTime.Parse(departureDateTimeStr);
            dbTicket.DepartureDateTime = departureDateTime;

            DateTime arrivalDateTime = DateTime.Parse(arrivalDateTimeStr);
            ticket.ArrivalDateTime = arrivalDateTime;

            TimeSpan flightDurationTime = TimeSpan.Parse(flightDuration);
            dbTicket.FlightDuration = flightDurationTime;


            dbTicket.DepartureAirportId = depAirId;
            dbTicket.ArrivalAirportId = arrAirId;
            dbTicket.SeatClassId = seatClassId;
            dbTicket.TicketPrice = ticket.TicketPrice;
            dbTicket.FlightDuration = ticket.FlightDuration;
            dbTicket.FlightNumber = ticket.FlightNumber;
            dbTicket.AirlineCompany = ticket.AirlineCompany;
            dbTicket.AirlineTicketDetail.FlightDescription = ticket.AirlineTicketDetail.FlightDescription;
            dbTicket.AirlineTicketDetail.HassMealService = ticket.AirlineTicketDetail.HassMealService;
            dbTicket.AirlineTicketDetail.MealPrice = ticket.AirlineTicketDetail.MealPrice;
            dbTicket.AirlineTicketDetail.MealDescription = ticket.AirlineTicketDetail.MealDescription;
            dbTicket.AirlineTicketDetail.HassBaggage = ticket.AirlineTicketDetail.HassBaggage;
            dbTicket.AirlineTicketDetail.BaggageAllowance = ticket.AirlineTicketDetail.BaggageAllowance;
            dbTicket.AirlineTicketDetail.Handluggage = ticket.AirlineTicketDetail.Handluggage;
            dbTicket.AirlineTicketDetail.IsReturn = ticket.AirlineTicketDetail.IsReturn;
            dbTicket.AirlineTicketDetail.IsTransfer = ticket.AirlineTicketDetail.IsTransfer;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {

            if(id == null)
            {
                return NotFound();

            }
            AirlineTicket dbTicket = await _db.AirlineTickets
             .Include(t => t.DepartureAirport)
             .Include(t => t.ArrivalAirport)
             .Include(t => t.ReturnAirport)
             .Include(t => t.TransferAirport)
             .Include(t => t.AirlineTicketDetail)
             .Include(t => t.SeatClass)
             .FirstOrDefaultAsync(x => x.Id == id);
            if (dbTicket == null)
            {
                return BadRequest();
            }
            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();

            return View(dbTicket);
        }

        #endregion
    }
}
