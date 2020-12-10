//Program:      Netd 3202 Lab 5 Salon Webpage
//Created by:   Jacky Yuan
//Date:         Dec 04, 2020
//Purpose:      Basic website for a hair salon
//Change log:   N/A


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Netd_Comm_Salon.Models;
using Netd_Comm_Salon.Data;
using Microsoft.AspNetCore.Authorization;

namespace Netd_Comm_Salon.Controllers
{
    public class appointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public appointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: appointments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var saloncontext = _context.appointment.Include(a => a.client).Include(a => a.stylist);
            return View(await saloncontext.ToListAsync());
        }

        // GET: appointments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment
                .Include(a => a.client)
                .Include(a => a.stylist)
                .FirstOrDefaultAsync(m => m.appointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: appointments/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["clientID"] = new SelectList(_context.client, "clientID", "clientID");
            ViewData["stylistID"] = new SelectList(_context.stylist, "stylistID", "fullname");
            return View();
        }

        // POST: appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointmentID,appointmentDate,stylistID,clientID")] appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["clientID"] = new SelectList(_context.client, "clientID", "clientID", appointment.clientID);
            ViewData["stylistID"] = new SelectList(_context.stylist, "stylistID", "fullname", appointment.stylistID);
            return View(appointment);
        }

        // GET: appointments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["clientID"] = new SelectList(_context.client, "clientID", "clientID", appointment.clientID);
            ViewData["stylistID"] = new SelectList(_context.stylist, "stylistID", "fullname", appointment.stylistID);
            return View(appointment);
        }

        // POST: appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointmentID,appointmentDate,stylistID,clientID")] appointment appointment)
        {
            if (id != appointment.appointmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!appointmentExists(appointment.appointmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["clientID"] = new SelectList(_context.client, "clientID", "clientID", appointment.clientID);
            ViewData["stylistID"] = new SelectList(_context.stylist, "stylistID", "fullname", appointment.stylistID);
            return View(appointment);
        }

        // GET: appointments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment
                .Include(a => a.client)
                .Include(a => a.stylist)
                .FirstOrDefaultAsync(m => m.appointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: appointments/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.appointment.FindAsync(id);
            _context.appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool appointmentExists(int id)
        {
            return _context.appointment.Any(e => e.appointmentID == id);
        }
    }
}
