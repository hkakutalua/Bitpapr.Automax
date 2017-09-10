using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Infrastructure.Services
{
    public class EfInvoiceNumberService : IInvoiceNumberService
    {
        public int NextNumber()
        {
            using (var context = new AutomaxContext())
            {
                int? maxNumber = context.Invoices.Max(i => (int?)i.Number);
                return (maxNumber == null) ? 1 : maxNumber.Value + 1;
            }
        }
    }
}
