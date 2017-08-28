using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Model
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Customer Customer { get; set; }
        public Vehicle VehicleToRepair { get; set; }
        public List<ServiceToProvide> ServicesToProvide { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
