using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Data;
using Employee.Models;

namespace Employee.Controllers
{
    public class EmpsController : Controller
    {
        private readonly EmployeeContext _context;

        public EmpsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Emps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Emp.ToListAsync());
        }
        
        // GET: Emps/Details/5 get the complete details about the employee
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _context.Emp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            var empp = await _context.Emp.Include(e => e.VacationCasual).SingleAsync(e => e.Id == id);
            var emp1 = await _context.Emp.Include(e => e.VacationSchedule).SingleAsync(e => e.Id == id);

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

        // GET: Emps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Birthdate,Gender,CasualBalance,CasualUsed,SchedualBalance,SchedualUsed")] DataFromViewModel dataFromViewModel)
        {
            Emp newEmployee = new Emp { FullName = dataFromViewModel.FullName, Email = dataFromViewModel.Email, Gender = dataFromViewModel.Gender, Birthdate = dataFromViewModel.Birthdate };

            VacationCasual VacationCasual = new VacationCasual { Balance = 7, EmpId = newEmployee.Id, Used = 0 };
            VacationSchedule VacationSchedule = new VacationSchedule { Balance = 14, EmpId = newEmployee.Id, Used = 0 };
            newEmployee.VacationCasual = VacationCasual;
            newEmployee.VacationSchedule = VacationSchedule;
            _context.Add(newEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Emps/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Birthdate,Gender,CasualBalance,CasualUsed,SchedualBalance,SchedualUsed")] DataFromViewModel dataFromViewModel)
        {
            Emp emp = new Emp { Id = id, FullName = dataFromViewModel.FullName, Email = dataFromViewModel.Email, Gender = dataFromViewModel.Gender, Birthdate = dataFromViewModel.Birthdate };
            VacationCasual vacationCasual = new VacationCasual { Id = id, Balance = dataFromViewModel.CasualBalance, EmpId = emp.Id, Used = dataFromViewModel.CasualUsed };
            VacationSchedule vacationSchedule = new VacationSchedule { Id = id, Balance = dataFromViewModel.SchedualBalance, EmpId = emp.Id, Used = dataFromViewModel.SchedualUsed };

            emp.VacationCasual = vacationCasual;
            emp.VacationSchedule = vacationSchedule;
            if (id != emp.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(emp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(emp.Id))
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

        // GET: Emps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _context.Emp
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var emp = await _context.Emp.FindAsync(id);
            _context.Emp.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpExists(int id)
        {
            return _context.Emp.Any(e => e.Id == id);
        }
    }
}
