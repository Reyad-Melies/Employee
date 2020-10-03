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
