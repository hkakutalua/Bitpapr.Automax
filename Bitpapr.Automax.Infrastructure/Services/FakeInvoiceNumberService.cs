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
    public class FakeInvoiceNumberService : IInvoiceNumberService
    {
        public int NextNumber()
        {
            int? next = FakeInvoiceData.GetData().Max(i => new Nullable<int>(i.Number + 1));
            return next ?? 1;
        }
    }
}
