using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Infrastructure
{
    public class AutomaxDbInitializer : DropCreateDatabaseIfModelChanges<AutomaxContext>
    {
        protected override void Seed(AutomaxContext context)
        {
            context.Employees.Add(new Employee
            {
                Id = Guid.NewGuid(),
                LoginName = "dek.oliveira",
                HashedPassword = "$2y$10$k8ZYjcr/ra69vF5GtVVBluQeTB1xh.SSCAqbOdBS7b/osA.sdvg6.", //meumundo1234
                FirstName = "Melkizidek",
                LastName = "Oliveira",
                EmployeeRole = EmployeeRole.Administrator
            });

            base.Seed(context);
        }
    }
}
