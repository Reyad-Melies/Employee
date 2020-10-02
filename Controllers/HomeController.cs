using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Employee.Models;
using Employee.Data;
using Microsoft.EntityFrameworkCore;
using Employee.Repository;
using Employee.Service;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly VacationService _vacationService; 
       // private readonly IEmpRepository _empRepository;
      //  private readonly IVacationRepository _vacationRepository;
       // private readonly EmployeeContext _context;
        public HomeController(EmployeeContext context, IVacationRepository vacationRepository, IEmpRepository empRepository, EmployeeService employeeService, VacationService vacationService)
        {
         //   _empRepository = empRepository;
         //   _vacationRepository = vacationRepository;
            _employeeService = employeeService;
            _vacationService = vacationService;
            //      _context = context;
        }
        public IActionResult Index(String SearchString)
        {
          if (!String.IsNullOrEmpty(SearchString))
            {
                return View(_employeeService.GetEmployee(Int32.Parse(SearchString)));
                //    _empRepository.GetEmployee(Int32.Parse(SearchString)));
            }
            return View(_employeeService.GetEmps());
            //_empRepository.GetEmps());

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _employeeService.GetEmployeeWithVacation((int)id);
           // _empRepository.GetEmployeeWithVacation((int)id);
          //  await _context.Emp.Include(e => e.Vacations).SingleAsync(e => e.Id == id);
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

            //     var vacation = await _vacationService.GetVacation((int)id);
            //     var emp = await _employeeService.GetEmp(vacation.EmpId);

            await _vacationService.RequestVacation((int)id);

            //_vacationRepository.GetVacation((int)id);
            //await _context.Vacation.FindAsync(id);

            // _empRepository.GetEmp(vacation.EmpId);
            //  await _context.Emp.FindAsync(vacation.EmpId);
         /*   if (vacation == null||emp==null)
            {
                return NotFound();
            }
            vacation.Used += 1;
            emp.Vacations = new List<Vacation>();
            emp.Vacations.Add(vacation);
            _empRepository.UpdateEmployee(emp);
         */   //_context.Update(emp);
         //   await _empRepository.SaveAsync();
            //await _context.SaveChangesAsync();
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

