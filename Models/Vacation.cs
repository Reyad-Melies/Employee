using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Vacation
    {
        public int Id { get; set; }

        
        public int EmpId { get; set; }
      
        public string Type { get; set; }
    
        public int Balance { get; set; }
       
        public int Used { get; set; }
        public Emp Emp { get; set; }
    }
}
