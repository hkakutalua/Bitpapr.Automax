using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(Guid id);
        IEnumerable<Employee> GetAll();
        void Save(Employee employee);
    }
}
