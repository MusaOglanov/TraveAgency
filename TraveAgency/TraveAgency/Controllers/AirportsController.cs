﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraveAgency.DAL;
using TraveAgency.Models;

namespace TraveAgency.Controllers
{
    [Authorize]

    public class AirportsController : Controller
    {
        private readonly AppDbContext _db;

        public AirportsController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Airport> airports = await _db.Airports.ToListAsync();
            return View(airports);
        }
        #endregion

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
        public async Task<IActionResult> Create(Airport airport)
        {
            bool IsExist = await _db.Airports.AnyAsync(t => t.Name == airport.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name has been used before");
                return View();
            }
            bool IsCodeExist = await _db.Airports.AnyAsync(t => t.Code == airport.Code);

            if (IsCodeExist)
            {
                ModelState.AddModelError("Code", "This Code has been used before");
                return View();
            }

            await _db.Airports.AddAsync(airport);
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
            Airport dbAirport = await _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAirport == null)
            {
                return BadRequest();
            }
            return View(dbAirport);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Airport airport)
        {
            if (id == null)
            {
                return NotFound();
            }
            Airport dbAirport = await _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
            if (dbAirport == null)
            {
                return BadRequest();
            }

            bool IsExist = await _db.Airports.AnyAsync(t => t.Name == airport.Name && t.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name has been used before");
                return View(dbAirport);
            }


            bool IsCodeExist = await _db.Airports.AnyAsync(t => t.Code == airport.Code && t.Id != id);
            if (IsCodeExist)
            {
                ModelState.AddModelError("Code", "This Code has been used before");
                return View(dbAirport);
            }
            dbAirport.Name = airport.Name;

            dbAirport.Country = airport.Country;
            dbAirport.City = airport.City;
            dbAirport.Code = airport.Code;
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
            Airport dbAirport = await _db.Airports.FirstOrDefaultAsync(t => t.Id == id);

            if (dbAirport == null)
            {
                return BadRequest();
            }

            if (dbAirport.IsDeactive)
            {
                dbAirport.IsDeactive = false;
            }
            else
            {
                dbAirport.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
