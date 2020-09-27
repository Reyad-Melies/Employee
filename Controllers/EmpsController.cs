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
        public async Task<IActionResult> Index(String SearchString)
        {

            var emp = _context.Emp.ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                var empp = _context.Emp.Where(s => s.Id == Int32.Parse(SearchString));
                //  Find(Int32.Parse(SearchString));

                return View(empp);
            }
            return View(emp);
        }
       
        // GET: Emps/Details/5
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
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Birthdate,Gender")] Emp emp)
        {
            if (ModelState.IsValid)
            {
                emp.Vacations = new List<Vacation>();
                Vacation vacationCasual = new Vacation { Balance = 7, EmpId = emp.Id, Used = 0, Type = "Schedual Vacation" };
                Vacation vacationSchedule = new Vacation { Balance = 14, EmpId = emp.Id, Used = 0, Type = "Casual Vacation" };
                emp.Vacations.Add(vacationSchedule);
                emp.Vacations.Add(vacationCasual);
                _context.Add(emp);
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVacation(int id,[Bind("Type,Balance,Used")] Vacation vacation)
        {     
            if (ModelState.IsValid)
            {
                vacation.EmpId = id;
                var emp = await _context.Emp.FindAsync(id);
                emp.Vacations = new List<Vacation>();
                emp.Vacations.Add(vacation);
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Emp, "Id", "FullName", vacation.EmpId);
            return View(vacation);
        }
        // GET: Emps/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Emps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(emp);
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

        // GET: Emps/Delete/5
        public async Task<IActionResult> DeleteVacation(int? id)
        {

            var vacation = await _context.Vacation
                .Include(v => v.Emp)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(vacation);
        }

        // POST: Emps/Delete/5
        [HttpPost, ActionName("DeleteVacation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVacationConfirmed(int id)
        {  
            var vacation = await _context.Vacation.FindAsync(id);
            int? x = vacation.EmpId;
             _context.Vacation.Remove(vacation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Vacations/Edit/5
        public async Task<IActionResult> EditVacation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
                var vacation = await _context.Vacation.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }
             return View(vacation);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                var va = await _context.Vacation.FindAsync(id);
                var emp= await _context.Emp.FindAsync(va.EmpId);
                emp.Vacations = new List<Vacation>();
                   emp.Vacations.Add(vacation);

                _context.Update(emp);
                    await _context.SaveChangesAsync();
                //   return View();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Emp, "Id", "FullName", vacation.EmpId);
            return View(vacation);
        }
        private bool EmpExists(int id)
        {
            return _context.Emp.Any(e => e.Id == id);
        }
    }
}
