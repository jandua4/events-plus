using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsPlus.Data;
using EventsPlus.Models;

namespace EventsPlus.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly EventsPlusContext _context;

        public AttendeesController(EventsPlusContext context)
        {
            _context = context;
        }

        // GET: Attendees
        public async Task<IActionResult> Index()
        {
            var eventsPlusContext = _context.Attendees.Include(a => a.Event);
            return View(await eventsPlusContext.ToListAsync());
        }

        // GET: Attendees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees
                .Include(a => a.Event)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AttendeeID == id);

            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        // GET: Attendees/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name");
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendeeID,Name,Phone,Email,EventID")] Attendee attendee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(attendee);
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
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", attendee.EventID);
            return View(attendee);
        }

        // GET: Attendees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", attendee.EventID);
            return View(attendee);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendeeID,Name,Phone,Email,EventID")] Attendee attendee)
        {
            if (id != attendee.AttendeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendeeExists(attendee.AttendeeID))
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
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "Name", attendee.EventID);
            return View(attendee);
        }

        // GET: Attendees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees
                .Include(a => a.Event)
                .FirstOrDefaultAsync(m => m.AttendeeID == id);
            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.AttendeeID == id);
        }
    }
}
