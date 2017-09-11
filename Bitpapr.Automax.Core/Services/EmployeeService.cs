using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.QueryTypes;
using Bitpapr.Automax.Core.Repositories;
using Bitpapr.Automax.Core.Security;

namespace Bitpapr.Automax.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IQueryEmployeesByRole _queryEmployeesByRole;
        private readonly IPasswordHasher _passwordHasher;

        public EmployeeService(IEmployeeRepository employeeRepository, IQueryEmployeesByRole queryEmployeesByRole,
            IPasswordHasher passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _queryEmployeesByRole = queryEmployeesByRole;
            _passwordHasher = passwordHasher;
        }

        public IEnumerable<Employee> GetAllRegularsAndManagers() =>
            _queryEmployeesByRole.GetByAllRegularsAndManagers();

        public void Insert(string loginName, string firstName, string lastName, string password,
            EmployeeRole role)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                LoginName = loginName,
                EmployeeRole = role,
                HashedPassword = _passwordHasher.HashPassword(password)
            };
            
            _employeeRepository.Save(employee);
        }

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
