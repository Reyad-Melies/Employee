using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class All
    {
        public int Id { get; set; }
        public Emp Empp { get; set; }
        public VacationCasual VacationCasual { get; set; }
        public VacationSchedule VacationSchedule { get; set; }
    }
}
