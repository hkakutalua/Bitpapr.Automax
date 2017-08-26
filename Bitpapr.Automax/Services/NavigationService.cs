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

    /// <summary>
    /// Class that view models can use to navigate and
    /// open windows in a decoupled way
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Open the specified window as modal dialog
        /// </summary>
        /// <param name="windowToNavigateTo"></param>
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
