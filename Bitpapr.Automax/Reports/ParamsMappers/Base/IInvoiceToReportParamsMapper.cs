using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Reports.ParamsMappers
{
    /// <summary>
    /// Interface for implementers that want to map Invoice data
    /// to a dictionary of parameters to send to a report
    /// </summary>
    public interface IInvoiceToReportParamsMapper
    {
        Dictionary<string, object> Map(Invoice invoice);
    }
}
