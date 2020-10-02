using Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository
{
   public interface IEmpRepository : IDisposable
    {
        // Returns list of employees without vacations
        List<Emp> GetEmps();

        // Returns an employee without vacations
        IEnumerable<Emp> GetEmployee(int Id);

         Task<Emp> GetEmp(int Id);

        // Returns an employee with vacations
        Task<Emp> GetEmployeeWithVacation(int Id);

        // Creates an employee
        void CreateEmployee(Emp employee);

        // Deletes an employee
        void DeleteEmployee(Emp employee);

        // Updates an employee
        void UpdateEmployee(Emp employee);

        Task<int> SaveAsync();


    }
}
