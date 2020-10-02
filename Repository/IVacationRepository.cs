using Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository
{
   public interface IVacationRepository
    {  
        // Returns vacation with specific id
        Task<Vacation> GetVacation(int Id);
    
        // Creates an vacation
        void Create(Vacation vacation);
        // Deletes an employee
        void DeleteVacation(Vacation vacation);

        // Updates an employee
       
        Task<int> SaveAsync();
    }
}
