using Bitpapr.Automax.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Infrastructure.Repositories
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        public Invoice GetByNumber(int number) =>
            FakeInvoiceData.GetData().FirstOrDefault(i => i.Number == number);

        public void Save(Invoice invoice)
        {
            var invoices = (List<Invoice>)FakeInvoiceData.GetData();
            
            // Inserts new invoice only if invoice's number isn't duplicated,
            // otherwise, just update existent
            if (invoices.Exists(i => i.Number != invoice.Number))
            {
                invoices.Add(invoice);
            }
            else
            {
                var foundInvoice = invoices.FirstOrDefault(i => i.Number == invoice.Number);
                foundInvoice = invoice;
            }
        }
    }

    internal static class FakeInvoiceData
    {
        private static List<Invoice> _invoices = new List<Invoice>();
        private static Invoice _dummyInvoice = new Invoice
        {
            Customer = new Customer
            {
                FirstName = "Henrick",
                LastName = "Kakutalua",
                PhoneNumber = "993 020 830",
                City = "Luanda",
                Neighborhood = "Vila de Viana"
            },
            VehicleToRepair = new Vehicle { Manufacturer = "Toyota", Model = "Yaris", PlateNumber = "LD-90-23-02" },
        };

        public static IEnumerable<Invoice> GetData() => _invoices;

        static FakeInvoiceData()
        {
            for (int i = 1; i <= 20; i++)
            {
                var invoice = new Invoice
                {
                    Number = i,
                    Customer = _dummyInvoice.Customer,
                    VehicleToRepair = _dummyInvoice.VehicleToRepair,
                    InvoiceDate = DateTime.Now
                };
                _invoices.Add(invoice);
            }
        }
    }
}
