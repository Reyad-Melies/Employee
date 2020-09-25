using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Employee.Models;
using Employee.Migrations;
using Employee.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeContext _context;
        int i = -10;
        public HomeController(EmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Index(String SearchString)
        { 
            var allViewModel = from e in _context.Emp
                               join d in _context.VacationCasuals on e.Id equals d.EmpId into table1
                               from d in table1
                               join i in _context.VacationSchedules on e.Id equals i.EmpId into table2
                               from i in table2
                               select new All { Empp = e, VacationCasual = d, VacationSchedule = i };

            if (!String.IsNullOrEmpty(SearchString))
            {
                int i = Int32.Parse(SearchString);
          allViewModel = allViewModel.Where(s => s.Empp.Id == Int32.Parse(SearchString));
            }
                return View(allViewModel);
        }
       public async Task<IActionResult> Requests(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.Emp.Include(e => e.VacationCasual).SingleAsync(e => e.Id == id);
            var emp1 = await _context.Emp.Include(e => e.VacationSchedule).SingleAsync(e => e.Id == id);
           DataFromViewModel dataFromViewModel = new DataFromViewModel
            {
                FullName = emp.FullName,
                SchedualBalance = emp1.VacationSchedule.Balance,
                SchedualUsed = emp1.VacationSchedule.Used,
                CasualBalance = emp.VacationCasual.Balance,
                CasualUsed = emp.VacationCasual.Used,
                EmpId = emp.VacationCasual.EmpId,
                Email = emp.Email,
                Gender = emp.Gender,
                Birthdate = emp.Birthdate,
                Id = emp.Id
            };
            if (emp == null)
            {
                return NotFound();
            }
            return View(dataFromViewModel);
        }
        // POST: Emps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult secduleVacation()
        {
            if (i != -1)
            {
                //    var emp =  _context.Emp.Include(e => e.VacationCasual).SingleAsync(e => e.Id == Int32.Parse(SearchString));
                VacationSchedule vacation = _context.VacationSchedules.FirstOrDefault(e => e.EmpId == i);
                vacation.Used--;

                _context.Update(vacation);
                _context.SaveChangesAsync();
                return View();
            }
            else return NotFound();
        }
        public IActionResult casualVacation()
        {
            if (i != -1)
            {
                //    var emp =  _context.Emp.Include(e => e.VacationCasual).SingleAsync(e => e.Id == Int32.Parse(SearchString));
                VacationSchedule vacation = _context.VacationSchedules.FirstOrDefault(e => e.EmpId == i);
                vacation.Used--;

                _context.Update(vacation);
                _context.SaveChangesAsync();
                return View();
            }
            else return NotFound();
        }
    }
}




























/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Employee.Models;
using Employee.Migrations;
using Employee.Data;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        int i=0;

        private readonly EmployeeContext _context;


        public HomeController(EmployeeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        { Emp emp = new Emp();
            
          *//*  var allViewModel = from e in _context.Emp
                               join d in _context.VacationCasuals on e.Id equals d.EmpId into table1
                               from d in table1 
                               join i in _context.VacationSchedules on e.Id equals i.EmpId into table2
                               from i in table2
                               select new All { Empp = e,  VacationCasual= d,  VacationSchedule= i };
            Emp emp = new Emp();*//*
            return View(emp);
           // return View(allViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id")] Emp employe)
        {
            i =employe.Id;
            return RedirectToAction("Privacy", "HomeController");
        }
        public async Task<IActionResult> Privacy()
        {
           *//* if (i == null)
            {
                return NotFound();
            }*//*

            var emp = await _context.Emp
                .FirstOrDefaultAsync(m => m.Id == i);
           *//* if (emp == null)
            {
                return NotFound();
            }*//*
            var empp = await _context.Emp.Include(e => e.VacationCasual).SingleAsync(e => e.Id == i);
            var emp1 = await _context.Emp.Include(e => e.VacationSchedule).SingleAsync(e => e.Id == i);

            DataFromViewModel dataFromViewModel = new DataFromViewModel
            {
                FullName = empp.FullName,
                SchedualBalance = emp1.VacationSchedule.Balance,
                SchedualUsed = emp1.VacationSchedule.Used,
                CasualBalance = empp.VacationCasual.Balance,
                CasualUsed = empp.VacationCasual.Used,
                EmpId = empp.VacationCasual.EmpId,
                Email = empp.Email,
                Gender = emp.Gender,
                Birthdate = empp.Birthdate,
                Id = empp.Id
            };
            
            return View(dataFromViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
*/