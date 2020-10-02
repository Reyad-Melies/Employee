using Employee.Data;
using Employee.Models;
using Employee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Service
{
    public class VacationService
    {
        private readonly IEmpRepository _empRepository;
        private readonly IVacationRepository _vacationRepository;

        public VacationService(EmployeeContext context, IVacationRepository vacationRepository, IEmpRepository empRepository)
        {
            _empRepository = empRepository;
            _vacationRepository = vacationRepository;
        }

        public async Task<Vacation> GetVacation(int Id)
        {
            return await _vacationRepository.GetVacation(Id);
        }

        public async Task CreateVacation(Vacation vacation)
        {
            _vacationRepository.Create(vacation);
            await _vacationRepository.SaveAsync();
        }
        public async Task EditVacation(Vacation vacation,int id)
        {
            var vac = await _vacationRepository.GetVacation(id);
            var emp = await _empRepository.GetEmp(vac.EmpId);
            emp.Vacations = new List<Vacation>();
            emp.Vacations.Add(vacation);
            _empRepository.UpdateEmployee(emp);
            await _empRepository.SaveAsync();
        }
        public async Task DeleteVacation(int id)
        {
            var vacation = await _vacationRepository.GetVacation(id);
            _vacationRepository.DeleteVacation(vacation);
            await _vacationRepository.SaveAsync();
        }
        public async Task RequestVacation(int id)
        {
            var vacation = await _vacationRepository.GetVacation((int)id);
            var emp = await _empRepository.GetEmp(vacation.EmpId);
            vacation.Used += 1;
            emp.Vacations = new List<Vacation>();
            emp.Vacations.Add(vacation);
            _empRepository.UpdateEmployee(emp);
            await _empRepository.SaveAsync();
        }

    }
}
