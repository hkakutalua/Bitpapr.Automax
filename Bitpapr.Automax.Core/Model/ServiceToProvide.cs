using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Model
{
    public class ServiceToProvide
    {
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public decimal ChargedPrice { get; set; }
        public string Comments { get; set; }

        public int InvoiceId { get; set; }
    }
}
