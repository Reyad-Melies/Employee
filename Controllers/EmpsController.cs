using Employee.Models;
using Employee.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Employee.Controllers
{
    public class EmpsController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly VacationService _vacationService;
        public EmpsController(EmployeeService employeeService, VacationService vacationService)
        {
            _employeeService = employeeService;
            _vacationService = vacationService;
        }
        // GET: Emps
        public IActionResult Index(String SearchString)
        {

            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(_employeeService.GetEmployee(Int32.Parse(SearchString)));
            }
            return View(_employeeService.GetEmps());
        }
        // GET: Emps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _employeeService.GetEmployeeWithVacation((int)id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // GET: Emps/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Birthdate,Gender")] Emp emp)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployee(emp);
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }
        public IActionResult CreateVacation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Vacations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVacation(int id, [Bind("Type,Balance,Used")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                vacation.EmpId = id;
                await _vacationService.CreateVacation(vacation);

   
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }
        // GET: Emps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _employeeService.GetEmployeeWithVacation((int)id);

     
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // POST: Emps/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Birthdate,Gender")] Emp emp)
        {
            if (id != emp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.EditEmployee(emp);
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // GET: Emps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _employeeService.GetEmp((int)id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        // POST: Emps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _employeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Emps/Delete/5
        public async Task<IActionResult> DeleteVacation(int? id)
        {

            var vacation = await _vacationService.GetVacation((int)id);
            return View(vacation);
        }

        // POST: Emps/Delete/5
        [HttpPost, ActionName("DeleteVacation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVacationConfirmed(int id)
        {
            await _vacationService.DeleteVacation(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: Vacations/Edit/5
        public async Task<IActionResult> EditVacation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _vacationService.GetVacation((int)id);
            if (vacation == null)
            {
                return NotFound();
            }
            return View(vacation);
        }

        // POST: Vacations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVacation(int id, [Bind("Id,Type,Balance,Used")] Vacation vacation)
        {
            if (id != vacation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _vacationService.EditVacation(vacation, id);
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }
    }
}
