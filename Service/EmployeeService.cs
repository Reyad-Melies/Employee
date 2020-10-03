using Employee.Data;
using Employee.Models;
using Employee.Repository;
using System.Collections.Generic;
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
        public IEnumerable<Emp> GetEmployee(int id)
        {
            return _empRepository.GetEmployee(id);
        }
        public async Task<Emp> GetEmp(int Id)
        {

            return await _empRepository.GetEmp(Id); ;
        }
        public List<Emp> GetEmps()
        {
            return _empRepository.GetEmps();
        }
        public async Task<Emp> GetEmployeeWithVacation(int id)
        {
            return await _empRepository.GetEmployeeWithVacation(id);
        }

        public async Task CreateEmployee(Emp employee)
        {
            _empRepository.CreateEmployee(employee);
            await _empRepository.SaveAsync();
        }
        public async Task EditEmployee(Emp emp)
        {
            _empRepository.UpdateEmployee(emp);
            await _empRepository.SaveAsync();
        }
        public async Task DeleteEmployee(int id)
        {
            var emp = await _empRepository.GetEmp((int)id);
            _empRepository.DeleteEmployee(emp);
            await _empRepository.SaveAsync();

        }


    }
}
