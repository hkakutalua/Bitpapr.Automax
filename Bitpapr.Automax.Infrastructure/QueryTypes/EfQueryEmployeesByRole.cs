using Bitpapr.Automax.Core.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Infrastructure.Repositories;

namespace Bitpapr.Automax.Infrastructure.QueryTypes
{
    public class EfQueryEmployeesByRole : IQueryEmployeesByRole
    {
        public IEnumerable<Employee> GetByAllRegularsAndManagers()
        {
            using (var context = new AutomaxContext())
            {
                return context.Employees
                        .Where(e => e.EmployeeRole == EmployeeRole.Regular ||
                               e.EmployeeRole == EmployeeRole.Manager)
                        .ToList();
            }
        }
    }
}
