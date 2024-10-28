using Information_System_ASP.Net.Data;
using Information_System_ASP.Net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Information_System_ASP.Net.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DriverController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchName, string searchCarReg, string searchEmployee)
        {
            var drivers = _context.Drivers
                                  .Include(d => d.Events)
                                  .AsQueryable();

            // Filter by Driver Name
            if (!string.IsNullOrEmpty(searchName))
            {
                drivers = drivers.Where(d => d.DriverName.Contains(searchName));
            }

            // Filter by Car Registration
            if (!string.IsNullOrEmpty(searchCarReg))
            {
                drivers = drivers.Where(d => d.CarReg.Contains(searchCarReg));
            }

            // Filter by Responsible Employee
            if (!string.IsNullOrEmpty(searchEmployee))
            {
                drivers = drivers.Where(d => d.ResponsibleEmployee.Contains(searchEmployee));
            }

            return View(await drivers.ToListAsync());
        }


        [HttpGet] // GET: Driver/Details/5
        public async Task<IActionResult> Details(int id, DateTime? fromDate, DateTime? toDate)
        {
            var driver = await _context.Drivers
                                       .Include(d => d.Events)
                                       .FirstOrDefaultAsync(m => m.DriverID == id);

            if (driver == null)
            {
                return NotFound();
            }

            // Filter events by date range
            var filteredEvents = driver.Events
                                       .Where(e => (!fromDate.HasValue || e.EventDate >= fromDate) &&
                                                   (!toDate.HasValue || e.EventDate <= toDate))
                                       .OrderBy(e => e.EventDate)
                                       .ToList();

            ViewBag.DriverName = driver.DriverName;
            ViewBag.DriverID = id;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(filteredEvents); // Pass filtered events directly to the view
        }






        // GET: Driver/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Fetch all employees and send them to the view as a dropdown list
            ViewBag.Employees = new SelectList(_context.Users.ToList(), "Name", "Name");
            return View();
        }


        // Create new driver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver driver)
        {
            if (ModelState.IsValid)
            {
                driver.NoteDate = DateTime.Now;  // Set NoteDate to current date and time
                driver.NoteDescription = "Created new driver";  // Set default note description

                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload the employees list if model validation fails
            ViewBag.Employees = new SelectList(_context.Users.ToList(), "Name", "Name", driver.ResponsibleEmployee);
            return View(driver);
        }


        // GET: Driver/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            // Fetch all employees and send them to the view as a dropdown list
            ViewBag.Employees = new SelectList(_context.Users.ToList(), "Name", "Name", driver.ResponsibleEmployee);
            return View(driver);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Driver driver)
        {
            if (id != driver.DriverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverID))
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

            ViewBag.Employees = new SelectList(_context.Users.ToList(), "Name", "Name", driver.ResponsibleEmployee);
            return View(driver);
        }




        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.DriverID == id);
        }


        [HttpGet]// Delete driver
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver); // Show a view with confirmation
        }

        // Handle confirmed delete request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
      
    }
}
