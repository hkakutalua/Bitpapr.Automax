using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.QueryTypes;
using Bitpapr.Automax.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Infrastructure.QueryTypes
{
    public class FakeQueryLastInvoices : IQueryLastInvoices
    {
        public Invoice GetLastIssued()
        {
            var invoices = FakeInvoiceData.GetData();
            return invoices.FirstOrDefault(
                i => i.Number == invoices.Max(invoice => invoice.Number));
        }

        public IEnumerable<Invoice> GetRecents(int maxNumberToRetrieve)
        {
            return FakeInvoiceData.GetData()
                .OrderBy(i => i.InvoiceDate)
                .Reverse()
                .Take(maxNumberToRetrieve);
        }
    }
}
