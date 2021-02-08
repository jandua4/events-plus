﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsPlus.Data;
using EventsPlus.Models;
using EventsPlus.ViewModels;

namespace EventsPlus.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsPlusContext _context;

        public EventsController(EventsPlusContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var eventsPlusContext = _context.Events
                .Include(a => a.EventType)
                .Include(a => a.Manager);
            return View(await eventsPlusContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get data of all related entities
            var @event = await _context.Events
                .Include(a => a.EventType)
                .Include(a => a.Manager)
                .Include(a => a.Attendees)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            // Get manager and event type IDs to be used in dropdown list
            ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "EventTypeID", "Type");
            ViewData["ManagerID"] = new SelectList(_context.Managers, "ManagerID", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Name,StartTime,Location,Duration,Description,EventTypeID,ManagerID")] Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "EventTypeID", "Type", @event.EventTypeID);
            ViewData["ManagerID"] = new SelectList(_context.Managers, "ManagerID", "Name", @event.ManagerID);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "EventTypeID", "Type", @event.EventTypeID);
            ViewData["ManagerID"] = new SelectList(_context.Managers, "ManagerID", "Name", @event.ManagerID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Name,StartTime,Location,Duration,Description,EventTypeID,ManagerID")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
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
            ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "EventTypeID", "Type", @event.EventTypeID);
            ViewData["ManagerID"] = new SelectList(_context.Managers, "ManagerID", "Name", @event.ManagerID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(a => a.EventType)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }

        // Method for returning events in order of StartTime - should show up in ascending order
        public async Task<IActionResult> Schedule()
        {
            // Select event from each item in Events
            var events = from e in _context.Events
                         .Include(a => a.EventType)
                         select e;
            events = events.OrderBy(e => e.StartTime);

            return View(await events.AsNoTracking().ToListAsync());
        }



        // For Attendees to register
        public async Task<IActionResult> Register(int id)
        {
            var @event = await _context.Attendees
                .Include(a => a.Event)
                .ThenInclude(a => a.EventType)
                .FirstOrDefaultAsync(a => a.EventID == id);

            return View(@event);
        }

        // Registration POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("AttendeeID,Name,Phone,Email,EventID")] Attendee attendee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(attendee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Schedule));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(attendee);
        }



    }
}
