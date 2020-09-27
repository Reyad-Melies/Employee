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

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private readonly EmployeeContext _context;
        public HomeController(EmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Index(String SearchString)
        {
            var emp = _context.Emp.ToList() ;
            if (!String.IsNullOrEmpty(SearchString))
            {
                var empp =  _context.Emp.Where(s => s.Id==Int32.Parse(SearchString));
                return View(empp);
            }
            return View(emp);
            
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.Emp.Include(e => e.Vacations).SingleAsync(e => e.Id == id);
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

            var vacation = await _context.Vacation.FindAsync(id);
            var emp = await _context.Emp.FindAsync(vacation.EmpId);
            if (vacation == null||emp==null)
            {
                return NotFound();
            }
            vacation.Used += 1;
            emp.Vacations = new List<Vacation>();
            emp.Vacations.Add(vacation);
            _context.Update(emp);
            await _context.SaveChangesAsync();
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

