using Employee.Models;
using Employee.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly VacationService _vacationService;
        public HomeController(EmployeeService employeeService, VacationService vacationService)
        {
            _employeeService = employeeService;
            _vacationService = vacationService;
        }
        public IActionResult Index(String SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(_employeeService.GetEmployee(Int32.Parse(SearchString)));
            }
            return View(_employeeService.GetEmps());
   
        }
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
        // GET: Vacations/Edit/5
        public async Task<IActionResult> Request(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            await _vacationService.RequestVacation((int)id);
        return RedirectToAction(nameof(Index));
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