using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.QueryTypes;
using Bitpapr.Automax.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Infrastructure.QueryTypes
{
    public class EfQueryLastInvoices : IQueryLastInvoices
    {
        public Invoice GetLastIssued()
        {
            using (var context = new AutomaxContext())
            {
                var invoices = context.Invoices;

                return invoices
                    .Include(i => i.ServicesToProvide)
                    .Include(i => i.Employee)
                    .FirstOrDefault(i => i.Number == invoices.Max(invoice => invoice.Number));
            }
        }

        public IEnumerable<Invoice> GetRecents(int maxNumberToRetrieve)
        {
            using (var context = new AutomaxContext())
            {
                return context.Invoices
                    .OrderByDescending(i => i.InvoiceDate)
                    .Take(maxNumberToRetrieve)
                    .Include(i => i.ServicesToProvide)
                    .Include(i => i.Employee)
                    .ToList();
            }
        }
    }
}
