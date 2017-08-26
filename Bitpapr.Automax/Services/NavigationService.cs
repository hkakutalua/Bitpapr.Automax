using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax.Services
{
    public enum WindowType
    {
        NewInvoiceWindow,
        EditServicesWindow
    };

    public class NavigationService
    {
        public void ShowWindowAsModal(WindowType windowToNavigateTo)
        {
            switch (windowToNavigateTo)
            {
                case WindowType.NewInvoiceWindow:
                    new NewInvoiceWindow().ShowDialog();
                    break;

                case WindowType.EditServicesWindow:
                    new EditServicesWindow().ShowDialog();
                    break;
            }
        }
    }
}
