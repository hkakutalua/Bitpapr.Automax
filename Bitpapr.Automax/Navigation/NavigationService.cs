using Bitpapr.Automax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax.Navigation
{
    /// <summary>
    /// Class that view models can use to navigate and open windows
    /// in a decoupled way
    /// </summary>
    public class NavigationService : INavigationService
    {
        public void ShowWindow(WindowType windowToNavigateTo, object parameter = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.NavigationParameter = parameter;
            window.Show();
        }
        
        public void ShowWindowAsModal(WindowType windowToNavigateTo, object parameter = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.NavigationParameter = parameter;
            window.ShowDialog();
        }
        
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
                case WindowType.LoginWindow:
                    return new LoginWindow();

                case WindowType.MainWindow:
                    return new MainWindow();

                case WindowType.NewInvoiceWindow:
                    return new NewInvoiceWindow();

                case WindowType.EditServicesWindow:
                    return new EditServicesWindow();

                case WindowType.ReportViewerWindow:
                    return new ReportViewerWindow();

                default:
                    throw new ArgumentException("The WindowType passed is invalid",
                        nameof(windowType));
            }
        }
    }
}
