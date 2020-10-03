using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Emp
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Gender { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }

}


