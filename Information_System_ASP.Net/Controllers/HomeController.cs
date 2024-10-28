using Information_System_ASP.Net.Data;
using Information_System_ASP.Net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Information_System_ASP.Net.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get the logged-in user
            var currentUser = await _userManager.GetUserAsync(User);

            // Create the view model
            var viewModel = new DashboardViewModel
            {
                Notifications = new List<Event>(),
                Drivers = new Dictionary<int, Driver>()
            };

            DateTime timeFilter = DateTime.Now.AddHours(currentUser.Role == "Admin" ? -24 : -12);

            // Fetch events
            viewModel.Notifications = await _context.Events
                .Where(e => e.EventDate >= timeFilter)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            // Fetch driver info for notifications
            var driverIds = viewModel.Notifications.Select(e => e.DriverID).Distinct();
            viewModel.Drivers = await _context.Drivers
                .Where(d => driverIds.Contains(d.DriverID))
                .ToDictionaryAsync(d => d.DriverID);

            // Fetch recent driver notes only for Admin
            if (User.IsInRole("Admin"))
            {
                DateTime recentDriverTime = DateTime.Now.AddHours(-24);
                viewModel.RecentDrivers = await _context.Drivers
                    .Where(d => d.NoteDate >= recentDriverTime)
                    .OrderByDescending(d => d.NoteDate)
                    .ToListAsync();
            }

            return View(viewModel);
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
