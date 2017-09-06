using Bitpapr.Automax.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Infrastructure.Repositories
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public Employee GetById(Guid id) =>
            FakeEmployeeData.GetData().FirstOrDefault(e => e.Id == id);

        public IEnumerable<Employee> GetAll() =>
            FakeEmployeeData.GetData();

        public void Save(Employee employee)
        {
            var employees = (List<Employee>)FakeEmployeeData.GetData();
            
            if (employees.Exists(e => e.Id != employee.Id))
            {
                employees.Add(employee);
            }
            else
            {
                var foundEmployee = employees.FirstOrDefault(e => e.Id == employee.Id);
                foundEmployee = employee;
            }
        }
    }

    internal static class FakeEmployeeData
    {
        private static List<Employee> employeeList = new List<Employee>
        {
            // That HashedPassword is '1234'
            new Employee { Id = Guid.NewGuid(), LoginName = "henrick.pedro", HashedPassword = "$2y$10$xBrLjjtxGvv/w6lFO1tzuOwS8/emX0IZJd4NZkQ.7wa74gFlgkqGG", FirstName = "Henrick", LastName = "Pedro", EmployeeRole = EmployeeRole.Administrator },

            new Employee { Id = Guid.NewGuid(), LoginName = "eulalio.antero", FirstName = "Eulálio", LastName = "Antero", EmployeeRole = EmployeeRole.Manager },
            new Employee { Id = Guid.NewGuid(), LoginName = "nanitamo.pedro", FirstName = "Nanitamo", LastName = "Pedro", EmployeeRole = EmployeeRole.Manager },
            new Employee { Id = Guid.NewGuid(), LoginName = "jorge.dacosta", FirstName = "Jorge", LastName = "DaCosta", EmployeeRole = EmployeeRole.Regular },
        };

        public static IEnumerable<Employee> GetData() => employeeList;
    }
}
