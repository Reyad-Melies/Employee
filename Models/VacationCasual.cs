using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class VacationCasual
    {
        public int Id { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        [DefaultValue(7)]
        public int Balance { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Used { get; set; }
        public Emp Emp { get; set; }
    }
}
