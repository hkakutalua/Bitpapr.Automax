using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Services
{
    public class InvoiceService : IInvoiceService
    {
        private static Invoice _testInvoice = new Invoice
        {
            Id = Guid.NewGuid(),
            Number = 1,
            Customer = new Customer
            {
                FirstName = "Henrick",
                LastName = "Kakutalua",
                PhoneNumber = "993 020 830",
                City = "Luanda",
                Neighboorhood = "Vila de Viana"
            },
            VehicleToRepair = new Vehicle { Manufacturer = "Toyota", Model = "Yaris", PlateNumber = "LD-90-23-02" },
            InvoiceDate = DateTime.Now
        };

        public Invoice GetByNumber(int number)
        {
            return _testInvoice;
        }

        public IEnumerable<Invoice> GetLastIssuedInvoices(int maximumInvoicesToRetrieve)
        {
            return new List<Invoice>
            {
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice,
                _testInvoice
            };
        }
    }
}
