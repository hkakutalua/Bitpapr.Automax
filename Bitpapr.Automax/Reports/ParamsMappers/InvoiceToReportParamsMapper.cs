using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Reports.ParamsMappers
{
    public class InvoiceToReportParamsMapper : IInvoiceToReportParamsMapper
    {
        public Dictionary<string, object> Map(Invoice invoice)
        {
            var dictionary = new Dictionary<string, object>();

            dictionary.Add("InvoiceNumber", invoice.Number);
            dictionary.Add("CustomerName", $"{invoice.Customer.FirstName} {invoice.Customer.LastName}");
            dictionary.Add("CustomerPhone", invoice.Customer.PhoneNumber);
            dictionary.Add("CustomerNeighborhood", invoice.Customer.Neighborhood);
            dictionary.Add("CustomerCity", invoice.Customer.City);
            dictionary.Add("VehicleBrand", invoice.VehicleToRepair.Manufacturer);
            dictionary.Add("VehicleModel", invoice.VehicleToRepair.Model);
            dictionary.Add("VehiclePlate", invoice.VehicleToRepair.PlateNumber);

            return dictionary;
        }
    }
}
