using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;

namespace Bitpapr.Automax.Core.Services
{
    public interface IInvoiceRepository
    {
        Invoice GetByNumber(int number);
        void Save(Invoice invoice);
        IList<Invoice> GetByEmployeeBetweenDates(
            Guid employeeId, DateTime startDate, DateTime endDate);
    }
}