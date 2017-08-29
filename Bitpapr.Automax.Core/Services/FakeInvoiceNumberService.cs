using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Services
{
    public class FakeInvoiceNumberService : IInvoiceNumberService
    {
        private readonly List<Invoice> _invoices;

        public FakeInvoiceNumberService(List<Invoice> invoices)
        {
            _invoices = invoices;
        }

        public int NextNumber()
        {
            int? next = _invoices.Max(i => new Nullable<int>(i.Number + 1));
            return next ?? 1;
        }
    }
}
