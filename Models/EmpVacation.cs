using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class EmpVacation
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int VacationId { get; set; }
    }
}
