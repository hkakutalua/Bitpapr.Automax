using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.QueryTypes
{
    public interface IQueryEmployeesByRole
    {
        IEnumerable<Employee> GetByAllRegularsAndManagers();
    }
}
