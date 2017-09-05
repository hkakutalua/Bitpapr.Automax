using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Services
{
    public interface ILoginService
    {
        bool EmployeeLogged { get; }
        Employee CurrentLoggedEmployee { get; }
        //bool LoginEmployee(string loginName, SecureString password);
        bool LoginEmployee(string loginName, string password);
        void LogoutCurrentEmployee();
    }
}
