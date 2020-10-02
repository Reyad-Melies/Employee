using Employee.Data;
using Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository
{
    public class VacationRepository : IVacationRepository, IDisposable
    {
        private bool _disposed = false;
        private  EmployeeContext _context;
        public VacationRepository(EmployeeContext context)
        {
            _context = context;

        }
        public  void Create(Vacation vacation)
        {         
             _context.Add(vacation); 
        }

        public void DeleteVacation(Vacation vacation)
        {
            _context.Vacation.Remove(vacation);
        }

        public async Task<Vacation> GetVacation(int Id)
        {
            var vacation = await _context.Vacation.FindAsync(Id);
            return vacation;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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
    }
}
