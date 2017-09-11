using Bitpapr.Automax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Bitpapr.Automax.Navigation
{
    /// <summary>
    /// Class that view models can use to navigate and open windows
    /// in a decoupled way
    /// </summary>
    public class NavigationService : INavigationService
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

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

            SetActiveWindowAsOwnerOf(window);

            window.ShowDialog();
        }
        
        public void ShowWindowAsModalForResult(WindowType windowToNavigateTo,
            EventHandler<ParameterPassingEventArgs> onArgumentPassing, object parameter = null)
        {
            BaseWindow window = CreateWindow(windowToNavigateTo);
            window.NavigationParameter = parameter;
            window.ParameterPassing += onArgumentPassing;

            SetActiveWindowAsOwnerOf(window);

            window.ShowDialog();
        }

        /// <summary>
        /// Set the current active window as the parent of the window passed
        /// </summary>
        /// <param name="windowToOwn"></param>
        private void SetActiveWindowAsOwnerOf(Window windowToOwn)
        {
            IntPtr activeWindow = GetActiveWindow();
            windowToOwn.Owner = Application.Current.Windows.OfType<BaseWindow>()
                .SingleOrDefault(win => new WindowInteropHelper(win).Handle == activeWindow);
        }

        private BaseWindow CreateWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.LoginWindow:
                    return new LoginWindow();

                case WindowType.VisualizeInvoiceWindow:
                    return new VisualizeInvoiceWindow();

                case WindowType.MainWindow:
                    return new MainWindow();

                case WindowType.NewInvoiceWindow:
                    return new NewInvoiceWindow();

                case WindowType.EditServicesWindow:
                    return new EditServicesWindow();

                case WindowType.ManageEmployeesWindow:
                    return new ManageEmployeesWindow();

                case WindowType.ReportViewerWindow:
                    return new ReportViewerWindow();

                case WindowType.NewEmployeeWindow:
                    return new NewEmployeeWindow();

                default:
                    throw new ArgumentException("The WindowType passed is invalid",
                        nameof(windowType));
            }
        }
    }
}
