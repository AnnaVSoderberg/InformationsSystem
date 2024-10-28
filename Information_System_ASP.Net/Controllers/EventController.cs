using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Information_System_ASP.Net.Data;
using Information_System_ASP.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Information_System_ASP.Net.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public EventController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // List all events for a specific driver
        public async Task<IActionResult> Index(int driverId, DateTime? fromDate, DateTime? toDate)
        {
            // Get the driver based on driverId
            var driver = await _context.Drivers.FindAsync(driverId);

            if (driver == null)
            {
                return NotFound();  // If driver doesn't exist, return a 404 page
            }

            // Pass the driver's name to the view
            ViewBag.DriverName = driver.DriverName;

            // Filter events based on driverId
            var events = _context.Events.Where(e => e.DriverID == driverId);

            if (fromDate.HasValue && toDate.HasValue)
            {
                events = events.Where(e => e.EventDate >= fromDate && e.EventDate <= toDate);
            }

            return View(await events.ToListAsync());
        }


        // Display the form for creating a new event
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get the logged-in user
            var loggedInUser = await _userManager.GetUserAsync(User);

            // Get all drivers from the database
            var drivers = await _context.Drivers.ToListAsync();

            if (loggedInUser.Role == "Employee")
            {
                // Find the driver the employee is linked to
                var linkedDriver = await _context.Drivers
                    .FirstOrDefaultAsync(d => d.ResponsibleEmployee == loggedInUser.Name);

                // Populate dropdown with drivers, pre-select the linked driver for the employee
                ViewBag.Drivers = new SelectList(drivers, "DriverID", "DriverName", linkedDriver?.DriverID);
                ViewBag.DriverID = linkedDriver?.DriverID; // Setting ViewBag.DriverID for the back link
            }
            else if (loggedInUser.Role == "Admin")
            {
                // Populate dropdown with drivers without pre-selecting any specific driver
                ViewBag.Drivers = new SelectList(drivers, "DriverID", "DriverName");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                // Get the current logged-in user
                var loggedInUser = await _userManager.GetUserAsync(User);
                @event.LoggedByEmployee = loggedInUser.Name;

                _context.Add(@event);
                await _context.SaveChangesAsync();

                // Recalculate TotalBeloppIn and TotalBeloppUt for the Driver
                await UpdateDriverTotals(@event.DriverID);

                return RedirectToAction(nameof(Index), new { driverId = @event.DriverID });
            }

            // Rebuild the driver list if validation fails
            var drivers = await _context.Drivers.ToListAsync();
            ViewBag.Drivers = new SelectList(drivers, "DriverID", "DriverName", @event.DriverID);
            return View(@event);
        }

        // Display the form to edit an existing event
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            // Load driver list for dropdown
            ViewBag.Drivers = new SelectList(_context.Drivers, "DriverID", "DriverName", @event.DriverID);
            return View(@event);
        }

        // Edit an existing event (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
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

                    // Update totals after editing
                    await UpdateDriverTotals(@event.DriverID);
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
                return RedirectToAction(nameof(Index), new { driverId = @event.DriverID });
            }

            var drivers = await _context.Drivers.ToListAsync();
            ViewBag.Drivers = new SelectList(drivers, "DriverID", "DriverName", @event.DriverID);
            return View(@event);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(History));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }

        // Add this action in EventController
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> History(DateTime? fromDate, DateTime? toDate, string driverName, string employeeName)
        {
            var eventsQuery = _context.Events.Include(e => e.Driver).AsQueryable();

            // Filter by date range
            if (fromDate.HasValue && toDate.HasValue)
            {
                eventsQuery = eventsQuery.Where(e => e.EventDate >= fromDate && e.EventDate <= toDate);
            }

            // Filter by driver name
            if (!string.IsNullOrEmpty(driverName))
            {
                eventsQuery = eventsQuery.Where(e => e.Driver.DriverName.Contains(driverName));
            }

            // Filter by employee name
            if (!string.IsNullOrEmpty(employeeName))
            {
                eventsQuery = eventsQuery.Where(e => e.LoggedByEmployee.Contains(employeeName));
            }

            // Fetch events matching the criteria
            var events = await eventsQuery.OrderByDescending(e => e.EventDate).ToListAsync();

            return View(events);
        }

        private async Task UpdateDriverTotals(int driverId)
        {
            var driver = await _context.Drivers
                                       .Include(d => d.Events)
                                       .FirstOrDefaultAsync(d => d.DriverID == driverId);

            if (driver != null)
            {
                // No need to set TotalBeloppIn and TotalBeloppUt because they are calculated properties.
                _context.Update(driver);
                await _context.SaveChangesAsync();
            }
        }

        // Show a list of drivers to choose from for viewing events
        public async Task<IActionResult> DriverListForEvents()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return View(drivers); // This view will list drivers with links to their events
        }

    }
}
