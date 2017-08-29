using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.InversionOfControl
{
    /// <summary>
    /// Helper class related to inversion of control configuration
    /// </summary>
    public static class IoC
    {
        private static UnityContainer container = new UnityContainer();

        /// <summary>
        /// Constructor, register all types
        /// </summary>
        static IoC()
        {
            container.RegisterType<INavigationService, NavigationService>();
            container.RegisterType<IInvoiceService, FakeInvoiceService>();

            container.RegisterType<EditServicesViewModel>();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<NewInvoiceViewModel>();
        }

        /// <summary>
        /// Resolve a type from container
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() => container.Resolve<T>();
    }
}
