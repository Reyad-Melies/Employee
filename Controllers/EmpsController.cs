using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Data;
using Employee.Models;
using Employee.Repository;
using System.Threading;
using Employee.Service;

namespace Employee.Controllers
{
    public class EmpsController : Controller
    {
        //private readonly IEmpRepository _empRepository;
        //private readonly IVacationRepository _vacationRepository;
        private readonly EmployeeService _employeeService;
        private readonly VacationService _vacationService;
        //     private readonly EmployeeContext _context;
        public EmpsController(EmployeeService employeeService,VacationService vacationService)
        {
          //  _empRepository = empRepository;
          //  _vacationRepository = vacationRepository;
            _employeeService = employeeService;
            _vacationService = vacationService;
     //       _context = context;
        }
        // GET: Emps
        public async Task<IActionResult> Index(String SearchString)
        {

            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(_employeeService.GetEmployee(Int32.Parse(SearchString)));
                //_empRepository.GetEmployee(Int32.Parse(SearchString)));
            }
            return View(_employeeService.GetEmps());
        }//_empRepository.GetEmps()

        // GET: Emps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _employeeService.GetEmployeeWithVacation((int)id);
                //await _empRepository.GetEmployeeWithVacation((int)id);
                //_context.Emp.Include(e => e.Vacations).SingleAsync(e => e.Id == id);
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
                await _employeeService.CreateEmployee(emp);
              //  _empRepository.CreateEmployee(emp);
              //  await _empRepository.SaveAsync();
    

               /* emp.Vacations = new List<Vacation>();
                Vacation vacationCasual = new Vacation { Balance = 7, EmpId = emp.Id, Used = 0, Type = "Schedual Vacation" };
                Vacation vacationSchedule = new Vacation { Balance = 14, EmpId = emp.Id, Used = 0, Type = "Casual Vacation" };
                emp.Vacations.Add(vacationSchedule);
                emp.Vacations.Add(vacationCasual);
                _context.Add(emp);*/
             //   await _context.SaveChangesAsync();
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
                await _vacationService.CreateVacation(vacation);

               // _vacationRepository.Create(vacation);
               // await _vacationRepository.SaveAsync();


                /*var emp = await _context.Emp.FindAsync(id);
                emp.Vacations = new List<Vacation>();
                emp.Vacations.Add(vacation);
                _context.Add(vacation);*/
                //  // _vacationRepository.Create(vacation,id);
                // await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
      //      ViewData["EmpId"] = new SelectList(_context.Emp, "Id", "FullName", vacation.EmpId);
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

            //await _empRepository.GetEmployeeWithVacation((int)id);
            //await _context.Emp.Include(e => e.Vacations).SingleAsync(e => e.Id == id);

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
                  await  _employeeService.EditEmployee(emp);
                  //  _empRepository.UpdateEmployee(emp);
                  //  await _empRepository.SaveAsync();
                 
                    
                    
                    //  _context.Update(emp);
                  //  await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {/*
                    if (!EmpExists(emp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
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
           // await _empRepository.GetEmp((int)id);
              //  await _context.Emp.FirstOrDefaultAsync(m => m.Id == id);
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
          //  _empRepository.DeleteEmployee(emp);
          //  await _empRepository.SaveAsync();
           


            // await _context.Emp.FindAsync(id);

            // _context.Emp.Remove(emp);
             //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Emps/Delete/5
        public async Task<IActionResult> DeleteVacation(int? id)
        {

            var vacation = await _vacationService.GetVacation((int)id);

            //await _vacationRepository.GetVacation((int)id); 
            //await _context.Vacation.Include(v => v.Emp).FirstOrDefaultAsync(m => m.Id == id);

            return View(vacation);
        }

        // POST: Emps/Delete/5
        [HttpPost, ActionName("DeleteVacation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVacationConfirmed(int id)
        {
            await _vacationService.DeleteVacation(id);
           /* var vacation = await _vacationRepository.GetVacation(id);
            _vacationRepository.DeleteVacation(vacation);
            await _vacationRepository.SaveAsync();*/


            //await _context.Vacation.FindAsync(id);
            // _context.Vacation.Remove(vacation);
            //await _context.SaveChangesAsync();
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
           // await _vacationRepository.GetVacation((int)id);
                //await _context.Vacation.FindAsync(id);
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
                await _vacationService.EditVacation(vacation, id);
                /*   var vac=await _vacationRepository.GetVacation(id);
                   var emp =await _empRepository.GetEmp(vac.EmpId);
                   emp.Vacations = new List<Vacation>();
                   emp.Vacations.Add(vacation);
                   _empRepository.UpdateEmployee(emp);
                   await _empRepository.SaveAsync();*/
                return RedirectToAction(nameof(Index));
            }
        //    ViewData["EmpId"] = new SelectList(_context.Emp, "Id", "FullName", vacation.EmpId);
            return View(vacation);
        }
       /* private bool EmpExists(int id)
        {
            return _context.Emp.Any(e => e.Id == id);
        }*/
    }
}
