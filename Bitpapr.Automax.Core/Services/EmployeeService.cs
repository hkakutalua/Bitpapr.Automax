using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.QueryTypes;
using Bitpapr.Automax.Core.Repositories;

namespace Bitpapr.Automax.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IQueryEmployeesByRole _queryEmployeesByRole;

        public EmployeeService(IEmployeeRepository employeeRepository, IQueryEmployeesByRole queryEmployeesByRole)
        {
            _employeeRepository = employeeRepository;
            _queryEmployeesByRole = queryEmployeesByRole;
        }

        public IEnumerable<Employee> GetAllRegularsAndManagers() =>
            _queryEmployeesByRole.GetByAllRegularsAndManagers();

        public void Insert(Employee employee) =>
            _employeeRepository.Save(employee);

        public void ActivateEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
            {
                employee.Active = true;
                _employeeRepository.Save(employee);
            }
        }

        public void DisableEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
            {
                employee.Active = false;
                _employeeRepository.Save(employee);
            }
        }
    }
}
