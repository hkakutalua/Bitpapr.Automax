using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Model
{
    public enum EmployeeRole
    {
        Regular,
        Manager,
        Administrator
    };

    public class Employee
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
