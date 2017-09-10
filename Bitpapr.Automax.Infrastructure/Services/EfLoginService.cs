using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Repositories;
using System.Security;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Infrastructure.Repositories;
using Bitpapr.Automax.Infrastructure.Security;

namespace Bitpapr.Automax.Infrastructure.Services
{
    public class EfLoginService : ILoginService
    {
        private Employee _currentLoggedEmployee;
        private bool _employeeLogged;

        public bool EmployeeLogged => _employeeLogged;
        public Employee CurrentLoggedEmployee => _currentLoggedEmployee;

        public bool LoginEmployee(string loginName, string password)
        {
            using (var context = new AutomaxContext())
            {
                var hasher = new PasswordHasher();

                var employee = context.Employees
                    .FirstOrDefault(e =>
                        e.LoginName == loginName &&
                        e.Active == true);

                if (employee != null)
                {
                    if (!hasher.VerifyPassword(password, employee.HashedPassword))
                        return false;

                    LogoutCurrentEmployee();
                    _currentLoggedEmployee = employee;
                    _employeeLogged = true;

                    return true;
                }

                return false; 
            }
        }

        public void LogoutCurrentEmployee()
        {
            _currentLoggedEmployee = null;
            _employeeLogged = false;
        }
    }
}
