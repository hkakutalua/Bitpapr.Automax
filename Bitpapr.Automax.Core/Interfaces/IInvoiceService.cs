using System.Collections.Generic;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Services
{
    public interface IInvoiceService
    {
        Invoice GetByNumber(int number);
        IEnumerable<Invoice> GetLastIssuedInvoices(int maximumInvoicesToRetrieve);
    }
}