using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Exceptions;
using Bitpapr.Automax.Core.QueryTypes;

namespace Bitpapr.Automax.Core.Services
{
    public class FakeInvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceNumberService _invoiceNumberService;
        private readonly IQueryLastInvoices _queryLastInvoices;
        private readonly ILoginService _loginService;

        public FakeInvoiceService(IInvoiceRepository invoiceRepository, IInvoiceNumberService invoiceNumberService,
            IQueryLastInvoices queryLastInvoices, ILoginService loginService)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceNumberService = invoiceNumberService;
            _queryLastInvoices = queryLastInvoices;
            _loginService = loginService;
        }

        public Invoice GetByNumber(int number) =>
            _invoiceRepository.GetByNumber(number);

        public Invoice GetLastIssuedInvoice() =>
            _queryLastInvoices.GetLastIssued();

        public IEnumerable<Invoice> GetLastIssuedInvoices(int maximumToRetrieve) =>
            _queryLastInvoices.GetRecents(maximumToRetrieve);

        public void AddNew(Customer customer, Vehicle vehicle, List<ServiceToProvide> servicesToProvide)
        {
            Invoice newInvoice = new Invoice
            {
                Number = _invoiceNumberService.NextNumber(),
                Customer = customer,
                Employee = _loginService.CurrentLoggedEmployee,
                VehicleToRepair = vehicle,
                ServicesToProvide = servicesToProvide,
                InvoiceDate = DateTime.Now
            };
            _invoiceRepository.Save(newInvoice);
        }
    }
}
