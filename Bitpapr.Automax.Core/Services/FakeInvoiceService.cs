using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Core.Services
{
    public class FakeInvoiceService : IInvoiceService
    {
        private readonly IInvoiceNumberService _invoiceNumberService;

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
            InvoiceDate = DateTime.Now
        };

        public FakeInvoiceService()
        {
            _invoiceNumberService = new FakeInvoiceNumberService(_invoices);

            if (_invoices.Count == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    var invoice = new Invoice
                    {
                        Id = Guid.NewGuid(),
                        Number = _invoiceNumberService.NextNumber(),
                        Customer = _dummyInvoice.Customer,
                        VehicleToRepair = _dummyInvoice.VehicleToRepair,
                        InvoiceDate = _dummyInvoice.InvoiceDate
                    };
                    _invoices.Add(invoice);
                }
            }
        }

        public Invoice GetById(Guid id) =>
            _invoices.FirstOrDefault(i => i.Id == id);

        public Invoice GetByNumber(int number) =>
            _invoices.FirstOrDefault(i => i.Number == number);

        public Invoice GetLastIssuedInvoice() =>
            _invoices.FirstOrDefault(
                i => i.Number == _invoices.Max(invoice => invoice.Number));

        public IEnumerable<Invoice> GetLastIssuedInvoices(int maximumToRetrieve) =>
            _invoices.Take(maximumToRetrieve);

        public void AddNew(Customer customer, Vehicle vehicle, List<ServiceToProvide> servicesToProvide)
        {
            Invoice newInvoice = new Invoice
            {
                Id = Guid.NewGuid(),
                Number = _invoiceNumberService.NextNumber(),
                Customer = customer,
                VehicleToRepair = vehicle,
                ServicesToProvide = servicesToProvide,
                InvoiceDate = DateTime.Now
            };
            _invoices.Add(newInvoice);
        }

        public void CancelInvoice(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
