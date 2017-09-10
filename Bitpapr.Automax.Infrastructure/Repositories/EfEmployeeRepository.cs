using Bitpapr.Automax.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using System.Data.Entity.Migrations;

namespace Bitpapr.Automax.Infrastructure.Repositories
{
    public class EfEmployeeRepository : IEmployeeRepository
    {
        public Employee GetById(Guid id)
        {
            using (var context = new AutomaxContext())
            {
                return context.Employees.FirstOrDefault(e => e.Id == id);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (var context = new AutomaxContext())
            {
                return context.Employees.ToList();
            }
        }

        public void Save(Employee employee)
        {
            using (var context = new AutomaxContext())
            {
                context.Employees.AddOrUpdate(employee);
                context.SaveChanges();
            }
        }
    }
}
