using CRUD_Operation.Data;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Controllers
{
    public class EmployeesController : Controller
    {
        public readonly EmployeeDBContext _DBValues;
        public EmployeesController(EmployeeDBContext _DBSetValues)
        {
            _DBValues = _DBSetValues;
        }
        public async  Task<IActionResult> Employees()
        {
            var employees = await _DBValues.Employees.ToListAsync();
            if (employees.Any())
            {
                var lastEmployeeId = employees[employees.Count - 1].Id;
                ViewBag.LastEmployeeId = lastEmployeeId + 1;
            }
            else
            {
                ViewBag.LastEmployeeId = 1; // or any default value
            }

            return View();
        }

        public async Task<ActionResult> EmployeesList()
        {
            var employees = await _DBValues.Employees.ToListAsync();
            return View(employees);
        }



       public async Task<ActionResult> AddEmployees(AddEmployeeModel AddModel)
        {

            var existingEmployee = await _DBValues.Employees.FirstOrDefaultAsync(e => e.Email == AddModel.Email);

            if (existingEmployee != null)
            {
                TempData["EmailExists"] = "Email ID already exists.";
                return RedirectToAction("Employees");
            }

            var employee = new Employee()
            {
                FirstName = AddModel.FirstName,
                LastName = AddModel.LastName,
                Email = AddModel.Email,
                PhoneNumber = AddModel.PhoneNumber,
                Department = AddModel.Department,   
                DepartmentID = AddModel.DepartmentID
            };

            await _DBValues.Employees.AddAsync(employee);
            await _DBValues.SaveChangesAsync();
            return RedirectToAction("EmployeesList");
        }


        [HttpGet]
        public async Task<IActionResult> EditEmployees(int id)
        {
            var employee = await _DBValues.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeModel()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department,
                    DepartmentID = employee.DepartmentID,
                };
                return await Task.Run(() => View("UpdateEmployeeDetails", viewModel));
            }
            return RedirectToAction("EmployeesList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEditEmployees(UpdateEmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateEmployeeDetails", model); // Return the view with validation errors
            }

            var existingEmployee = await _DBValues.Employees
                .Where(e => e.Email == model.Email && e.Id != model.Id)
                .FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                TempData["EmailExists"] = "Email ID already exists.";
                return RedirectToAction("EditEmployees", new { id = model.Id });
            }

            var employee = await _DBValues.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Department = model.Department;
                employee.DepartmentID = model.DepartmentID;

                await _DBValues.SaveChangesAsync();
                return RedirectToAction("EmployeesList");
            }

            return RedirectToAction("EmployeesList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            var employee = await _DBValues.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                _DBValues.Employees.Remove(employee);
                await _DBValues.SaveChangesAsync();

                // Update IDs of remaining employees
                await ReindexEmployeeIdsAsync();
            }

            if (!await _DBValues.Employees.AnyAsync())
            {
                await TruncateEmployeesTableAsync();
            }
            return RedirectToAction("EmployeesList");
        }

        // Method to reindex employee IDs
        private async Task ReindexEmployeeIdsAsync()
        {
            var employees = await _DBValues.Employees.OrderBy(e => e.Id).ToListAsync();
            int newId = 1;

            foreach (var employee in employees)
            {
                employee.Id = newId++;
            }

            await _DBValues.SaveChangesAsync();
        }
        private async Task TruncateEmployeesTableAsync()
        {
            await _DBValues.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Employees");
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeDetails(int id)
        {
            var employee = await _DBValues.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeModel()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department,
                    DepartmentID = employee.DepartmentID,
                };
                return await Task.Run(() => View("ViewEmployeeDetails", viewModel));
            }
            return RedirectToAction("EmployeesList");

        }
    }
}
