using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class DataFromViewModel
    {

        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public int EmpId { get; set; }

        [DefaultValue(7)]
        public int CasualBalance { get; set; }
        [DefaultValue(0)]
        public int CasualUsed { get; set; }
        [DefaultValue(14)]
        public int SchedualBalance { get; set; }
        [DefaultValue(0)]
        public int SchedualUsed { get; set; }



    }
}
