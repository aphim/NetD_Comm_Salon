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
    public class clientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public clientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: clients
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.client.ToListAsync());
        }

        // GET: clients/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client
                .FirstOrDefaultAsync(m => m.clientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: clients/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("clientID,clientFName,clientLName,clientPhonenumber,clientEmail")] client client)
        {
            if (client.vaildateInfo(client.clientFName, client.clientLName, client.clientPhonenumber, client.clientEmail) == true)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                return View(client);
            }
            else
            {
                return View("fail");
            }
        }

        // GET: clients/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("clientID,clientFName,clientLName,clientPhonenumber,clientEmail")] client client)
        {
            if (id != client.clientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!clientExists(client.clientID))
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
            return View(client);
        }

        // GET: clients/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client
                .FirstOrDefaultAsync(m => m.clientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: clients/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.client.FindAsync(id);
            _context.client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool clientExists(int id)
        {
            return _context.client.Any(e => e.clientID == id);
        }
    }
}
