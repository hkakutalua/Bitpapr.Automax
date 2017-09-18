using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitpapr.Automax.ViewModels
{
    public class MainViewModel : BaseWindowViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceRepository _invoiceRepository;

        private const string _reportLocation = "/Reports/FinancialReport.rdlc";

        public int CurrentInvoiceIndex { get; set; } = -1;
        public ObservableCollection<Invoice> LastIssuedInvoices { get; set; }

        public ICommand GetLastInvoicesCommand { get; set; }
        public ICommand NewInvoiceCommand { get; set; }
        public ICommand VisualizeInvoiceCommand { get; set; }
        public ICommand VisualizeEmployeesCommand { get; set; }
        public ICommand RunFinancialReportCommand { get; set; }

        public MainViewModel(IInvoiceService invoiceService, ILoginService loginService,
            INavigationService navigationService, IInvoiceRepository invoiceRepository)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _invoiceService = invoiceService;
            _invoiceRepository = invoiceRepository;

            LastIssuedInvoices = new ObservableCollection<Invoice>();
            GetLastInvoicesCommand = new RelayCommand(ExecuteGetLastInvoices);
            NewInvoiceCommand = new RelayCommand(ExecuteNewInvoice);
            VisualizeInvoiceCommand = new RelayCommand(ExecuteVisualizeInvoice, CanVisualizeInvoice);
            VisualizeEmployeesCommand = new RelayCommand(ExecuteVisualizeEmployees, CanVisualizeEmployees);
            RunFinancialReportCommand = new RelayCommand(ExecuteRunFinancialReport);

            ExecuteGetLastInvoices();
        }

        public override void OnWindowClosing(CancelEventArgs args)
        {
            _navigationService.ShowWindow(WindowType.LoginWindow);
        }

        private void ExecuteGetLastInvoices()
        {
            var invoices = _invoiceService.GetLastIssuedInvoices(50);

            LastIssuedInvoices.Clear();
            foreach (Invoice invoice in invoices)
            {
                LastIssuedInvoices.Add(invoice);
            }
        }

        private void ExecuteNewInvoice()
        {
            _navigationService.ShowWindowAsModalForResult(
                WindowType.NewInvoiceWindow, (s, e) =>
                {
                    if (e.Parameter is WindowResult)
                    {
                        if (((WindowResult)e.Parameter) == WindowResult.Success)
                            ExecuteGetLastInvoices();
                    }
                });
        }

        private void ExecuteVisualizeInvoice()
        {
            _navigationService.ShowWindowAsModal(WindowType.VisualizeInvoiceWindow,
                LastIssuedInvoices[CurrentInvoiceIndex]);
        }

        private bool CanVisualizeInvoice() => !(CurrentInvoiceIndex == -1);

        private void ExecuteVisualizeEmployees()
        {
            _navigationService.ShowWindowAsModal(WindowType.ManageEmployeesWindow);
        }

        private bool CanVisualizeEmployees()
        {
            EmployeeRole currentEmployeeRole = _loginService.CurrentLoggedEmployee.EmployeeRole;
            return currentEmployeeRole == EmployeeRole.Administrator;
        }

        private void ExecuteRunFinancialReport()
        {
            Employee employee = _loginService.CurrentLoggedEmployee;
            Guid employeeId = employee.Id;
            string employeeName = employee.FirstName + " " + employee.LastName;

            var invoices = _invoiceRepository.GetByEmployeeBetweenDates(
                employeeId, DateTime.Today, DateTime.Now);

            if (invoices.Count == 0)
                return;

            var reportData = new ReportData
            {
                ReportLocation = _reportLocation,
                DataSourceName = "Invoices",
                DataSourceValue = invoices
            };
            reportData.ReportParameters.Add("EmployeeName", employeeName);
            reportData.ReportParameters.Add("Date", DateTime.Now);

            _navigationService.ShowWindowAsModal(WindowType.ReportViewerWindow,
                reportData);
        }
    }
}
