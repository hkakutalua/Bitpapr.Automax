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
    public class LoginService : ILoginService
    {
        private Employee _currentLoggedEmployee;
        private bool _employeeLogged;

        public bool EmployeeLogged => _employeeLogged;
        public Employee CurrentLoggedEmployee => _currentLoggedEmployee;

        //public bool LoginEmployee(string loginName, SecureString password)
        //{
        //    var secureStringHandler = new SecureStringHandler();
        //    return LoginEmployee(loginName, secureStringHandler.Unsecure(password));
        //}

        public bool LoginEmployee(string loginName, string password)
        {
            var passwordHasher = new PasswordHasher();
            string hashedPassword = passwordHasher.HashPassword(password);

            var employee = FakeEmployeeData.GetData()
                .FirstOrDefault(e => e.LoginName == loginName && e.HashedPassword == hashedPassword);
            if (employee != null)
            {
                LogoutCurrentEmployee();
                _currentLoggedEmployee = employee;
                _employeeLogged = true;
                return true;
            }

            return false;
        }

        public void LogoutCurrentEmployee()
        {
            _currentLoggedEmployee = null;
            _employeeLogged = false;
        }
    }
}
