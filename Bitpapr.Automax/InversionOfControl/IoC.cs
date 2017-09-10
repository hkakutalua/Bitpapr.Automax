using Bitpapr.Automax.Core.QueryTypes;
using Bitpapr.Automax.Core.Repositories;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Infrastructure.QueryTypes;
using Bitpapr.Automax.Infrastructure.Repositories;
using Bitpapr.Automax.Infrastructure.Services;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Reports.ParamsMappers;
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
            // UI Mapping (View Models)
            container.RegisterType<EditServicesViewModel>();
            container.RegisterType<MainViewModel>();
            container.RegisterType<NewInvoiceViewModel>();
            container.RegisterType<LoginViewModel>();
            container.RegisterType<ManageEmployeesViewModel>();

            // UI Mapping (Services)
            container.RegisterType<INavigationService, NavigationService>();
            container.RegisterType<IDialogService, DialogService>();
            container.RegisterType<ILoginService, LoginService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IInvoiceToReportParamsMapper, InvoiceToReportParamsMapper>();

            // Core Mapping (Services and Repositories)
            container.RegisterType<IEmployeeRepository, FakeEmployeeRepository>();
            container.RegisterType<IInvoiceRepository, FakeInvoiceRepository>();
            container.RegisterType<IInvoiceService, FakeInvoiceService>();
            container.RegisterType<IInvoiceNumberService, FakeInvoiceNumberService>();
            container.RegisterType<IEmployeeService, FakeEmployeeService>();

            // Core Mapping (Query Types)
            container.RegisterType<IQueryLastInvoices, FakeQueryLastInvoices>();
            container.RegisterType<IQueryEmployeesByRole, FakeQueryEmployeesByRole>();
        }

        /// <summary>
        /// Resolve a type from container
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() => container.Resolve<T>();
    }
}
