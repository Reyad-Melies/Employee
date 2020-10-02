using Employee.Data;
using Employee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Service
{
    public class EmployeeService
    {
        private readonly IEmpRepository _empRepository;
        private readonly IVacationRepository _vacationRepository;
        public EmployeeService(EmployeeContext context, IVacationRepository vacationRepository, IEmpRepository empRepository)
        {
            _empRepository = empRepository;
            _vacationRepository = vacationRepository;
        }
        
    }
}
