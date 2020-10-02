using Employee.Data;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository
{       
    public class EmpRepository : IEmpRepository,IDisposable
    {
        private EmployeeContext _context;
        private bool _disposed = false;
        public EmpRepository(EmployeeContext context)
        {
            _context = context;

        }
        
        public void CreateEmployee(Emp employee)
        {
            employee.Vacations = new List<Vacation>();
            Vacation vacationCasual = new Vacation { Balance = 7, EmpId = employee.Id, Used = 0, Type = "Schedual Vacation" };
            Vacation vacationSchedule = new Vacation { Balance = 14, EmpId = employee.Id, Used = 0, Type = "Casual Vacation" };
            employee.Vacations.Add(vacationSchedule);
            employee.Vacations.Add(vacationCasual);
            _context.Emp.Add(employee);
        }

        public void DeleteEmployee(Emp employee)
        {
            _context.Remove(employee);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
      //  IEnumerable<Employee.Models.Emp>
        public IEnumerable<Emp> GetEmployee(int Id)
        {
            return  _context.Emp.Where(s => s.Id == Id);
        }

        public async Task<Emp> GetEmployeeWithVacation(int Id)
        {
            return await _context.Emp.Include(e => e.Vacations).SingleAsync(e => e.Id == Id);
         
        }

        public  List<Emp> GetEmps()
        {
            return  _context.Emp.ToList();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void UpdateEmployee(Emp employee)
        {
            _context.Update(employee);
        }

        public async Task<Emp> GetEmp(int Id)
        {   
            
            return await _context.Emp.FindAsync(Id); ;
        }
    }
}
