using Bitpapr.Automax.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Bitpapr.Automax.Infrastructure.Repositories
{
    public class EfInvoiceRepository : IInvoiceRepository
    {
        public Invoice GetByNumber(int number)
        {
            using (var context = new AutomaxContext())
            {
                return context.Invoices
                        .Include(i => i.Employee)
                        .Include(i => i.ServicesToProvide)
                        .FirstOrDefault(i => i.Number == number); 
            }
        }

        public void Save(Invoice invoice)
        {
            using (var context = new AutomaxContext())
            {
                context.Employees.Attach(invoice.Employee);
                context.Invoices.AddOrUpdate(invoice);
                context.SaveChanges();
            }
        }
    }
}
