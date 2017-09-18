using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Reports.DataModel
{
    public class FinancialReportData
    {
        public string EmployeeName { get; set; }
        public DateTime ReportDate { get; set; }
        public List<Invoice> Invoices { get; private set; }
    }
}
