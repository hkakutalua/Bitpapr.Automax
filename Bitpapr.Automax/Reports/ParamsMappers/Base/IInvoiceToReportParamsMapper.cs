using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Reports.ParamsMappers
{
    public interface IInvoiceToReportParamsMapper
    {
        Dictionary<string, object> Map(Invoice invoice);
    }
}
