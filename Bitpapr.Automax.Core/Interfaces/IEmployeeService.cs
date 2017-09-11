using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllRegularsAndManagers();
        void Insert(string loginName, string firstName, string lastName, string password,
            EmployeeRole role);
        void ActivateEmployee(Guid id);
        void DisableEmployee(Guid id);
    }
}
