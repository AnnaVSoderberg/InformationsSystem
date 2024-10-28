using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Information_System_ASP.Net.Models;

namespace Information_System_ASP.Net.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        // GET: Employee List
        public IActionResult Index()
        {
            var employees = _userManager.Users;
            return View(employees);
        }

        // GET: Employee/Create
        public IActionResult Create() => View();

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, string password)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(password) && password.Length >= 6)
            {
                var result = await _userManager.CreateAsync(employee, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employee, employee.Role);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            else
            {
                ModelState.AddModelError("", "Password must be at least 6 characters long.");
            }
            return View(employee);
        }

        // GET: Employee/Edit
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            return employee == null ? NotFound() : View(employee);
        }

        // POST: Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.UpdateAsync(employee);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(employee);
        }

        // GET: Employee/Delete
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            return employee == null ? NotFound() : View(employee);
        }

        // POST: Employee/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            if (employee != null) await _userManager.DeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
