using System.Collections.Generic;
using Bitpapr.Automax.Core.Model;

namespace Bitpapr.Automax.Core.QueryTypes
{
    public interface IQueryLastInvoices
    {
        Invoice GetLastIssued();
        IEnumerable<Invoice> GetRecents(int maxNumberToRetrieve);
    }
}