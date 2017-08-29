using Bitpapr.Automax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax.Navigation
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
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Open the specified window as modal and optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="parameter">The parameter to pass to the windows</param>
        public void ShowWindowAsModal(WindowType windowToNavigateTo, object parameter = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.NavigationParameter = parameter;
            window.ShowDialog();
        }

        /// <summary>
        /// Open the specified window as modal, and listen for any results from
        /// the window. Optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="onArgumentPassing">The delegate called when there are results available</param>
        /// <param name="parameter">The parameter to pass to the windows</param>
        public void ShowWindowAsModalForResult(WindowType windowToNavigateTo,
            EventHandler<ParameterPassingEventArgs> onArgumentPassing, object parameter = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.NavigationParameter = parameter;
            window.ParameterPassing += onArgumentPassing;
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
