using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string Balance { get; set; }
        public string used { get; set; }
        public string type { get; set; }
    }
}
