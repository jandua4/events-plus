using EventsPlus.Data;
using EventsPlus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            // Sort Parameters
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TypeSort"] = sortOrder == "Type" ? "type_desc" : "Type";
            // Search box filter
            ViewData["CurrentFilter"] = searchString;

            // Page number is 1 unless there's a search string
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Include related entities
            var events = from e in _context.Events
            .Include(a => a.EventType)
            .Include(a => a.Manager)
                         select e;

            // Search function
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Name.Contains(searchString));
            }

            // Switch case for sorting results
            switch (sortOrder)
            {
                case "name_desc":
                    events = events.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    events = events.OrderBy(s => s.StartTime);
                    break;
                case "date_desc":
                    events = events.OrderByDescending(s => s.StartTime);
                    break;
                case "Type":
                    events = events.OrderBy(s => s.EventType.Type);
                    break;
                case "type_desc":
                    events = events.OrderByDescending(s => s.EventType.Type);
                    break;
                default:
                    events = events.OrderBy(s => s.EventID);
                    break;
            }
            // Number of records per page before paginating
            int pageSize = 10;

            // Return view with parameters
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Return 404 if ID is null
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

            // If event doesn't exist, return 404
            if (@event == null)
            {
                return NotFound();
            }

            // Returns @event to user => see var @event above
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
                // Check model state is valid
                if (ModelState.IsValid)
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            // Catch errors
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            // Dropdown list of Event Types and Managers
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

            // If the model state is valid, attempt to save the changes in the database.
            // Else, throw an error
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
                // Redirect user to Index
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
            // Find event by ID, delete from database context
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }

        // Events Schedule
        // And Events Registration for Attendees
        // Method for returning events in order of StartTime - should show up in ascending order
        public async Task<IActionResult> Schedule(string searchString, string currentFilter, int? pageNumber)
        {
            // Search box filter
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Select event from each item in Events
            var events = from e in _context.Events
                         .Include(a => a.EventType)
                         select e;

            // Search function
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Name.Contains(searchString));
            }

            // Order by date - turns events into a schedules order
            events = events.OrderBy(e => e.StartTime);

            // Max 10 events per page
            int pageSize = 10;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // For Attendees to register
        public async Task<IActionResult> Register(int id)
        {
            ViewData["EventID"] = id;
            var @event = await _context.Attendees
                .Include(a => a.Event)
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
                // Add attendee details if model state is valid
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
