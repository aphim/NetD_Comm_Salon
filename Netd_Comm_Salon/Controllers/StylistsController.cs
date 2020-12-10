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

namespace NetD_lab5_Salon.Controllers
{
    public class StylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stylists
        public async Task<IActionResult> Index()
        {
            return View(await _context.stylist.ToListAsync());
        }

        // GET: Stylists/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _context.stylist
                .FirstOrDefaultAsync(m => m.stylistID == id);
            if (stylist == null)
            {
                return NotFound();
            }

            return View(stylist);
        }

        // GET: Stylists/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stylistID,stylistFName,stylistLName,stylistExt")] Stylist stylist)
        {
            if (stylist.vaildatestylst(stylist.stylistFName, stylist.stylistLName, stylist.stylistExt))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stylist);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(stylist);
            }
            else
            {
                return View("fail");
            }
        }

        // GET: Stylists/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _context.stylist.FindAsync(id);
            if (stylist == null)
            {
                return NotFound();
            }
            return View(stylist);
        }

        // POST: Stylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stylistID,stylistFName,stylistLName,stylistExt")] Stylist stylist)
        {
            if (id != stylist.stylistID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StylistExists(stylist.stylistID))
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
            return View(stylist);
        }

        // GET: Stylists/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _context.stylist
                .FirstOrDefaultAsync(m => m.stylistID == id);
            if (stylist == null)
            {
                return NotFound();
            }

            return View(stylist);
        }

        // POST: Stylists/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stylist = await _context.stylist.FindAsync(id);
            _context.stylist.Remove(stylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StylistExists(int id)
        {
            return _context.stylist.Any(e => e.stylistID == id);
        }
    }
}
