using Bitpapr.Automax.ViewModels;
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
    /// Class that view models can use to navigate and open windows
    /// in a decoupled way
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Open the specified window as modal and optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="argument">The argument to pass to the windows</param>
        public void ShowWindowAsModal(WindowType windowToNavigateTo, object argument = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.Argument = argument;
            window.ShowDialog();
        }

        /// <summary>
        /// Open the specified window as modal, and listen for any results from
        /// the window. Optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="onArgumentPassing">The delegate called when there are results available</param>
        /// <param name="argument"></param>
        public void ShowWindowAsModalForResult(WindowType windowToNavigateTo,
            EventHandler<ArgumentPassingEventArgs> onArgumentPassing, object argument = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.Argument = argument;
            window.ArgumentPassing += onArgumentPassing;
            window.ShowDialog();
        }

        private BaseWindow CreateWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.NewInvoiceWindow:
                    return new NewInvoiceWindow();

                case WindowType.EditServicesWindow:
                    return new EditServicesWindow();

                default:
                    throw new ArgumentException("The WindowType passed is invalid",
                        nameof(windowType));
            }
        }
    }
}
