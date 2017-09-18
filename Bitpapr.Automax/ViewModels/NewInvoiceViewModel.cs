using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Exceptions;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Reports.ParamsMappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class NewInvoiceViewModel : BaseWindowViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceToReportParamsMapper _invoiceToReportParamsMapper;
        private readonly IDialogService _dialogService;

        public Employee LoggedInEmployee { get; set; }
        public Customer Customer { get; set; }
        public Vehicle VehicleToRepair { get; set; }
        public decimal TotalCost { get; set; }

        public ObservableCollection<ServiceToProvide> ServicesToProvide { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand EditServicesCommand { get; set; }

        public NewInvoiceViewModel(IInvoiceService invoiceService, IInvoiceToReportParamsMapper invoiceToReportParamsMapper,
            IDialogService dialogService, INavigationService navigationService)
        {
            _invoiceService = invoiceService;
            _invoiceToReportParamsMapper = invoiceToReportParamsMapper;
            _dialogService = dialogService;
            _navigationService = navigationService;

            ServicesToProvide = new ObservableCollection<ServiceToProvide>();
            EditServicesCommand = new RelayCommand(ExecuteEditServices);
            ConfirmCommand = new RelayCommand(ExecuteConfirmNewInvoice, CanExecuteConfirmNewInvoice);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());

            Customer = new Customer();
            VehicleToRepair = new Vehicle();
        }

        private void ExecuteEditServices()
        {
            _navigationService.ShowWindowAsModalForResult(
                WindowType.EditServicesWindow,
                OnEditServicesArgumentPassing,
                ServicesToProvide);
        }

        private void ExecuteConfirmNewInvoice()
        {
            _invoiceService.AddNew(Customer, VehicleToRepair,
                ServicesToProvide.ToList());

            Invoice lastIssuedInvoice = _invoiceService.GetLastIssuedInvoice();
            var reportParams = _invoiceToReportParamsMapper.Map(lastIssuedInvoice);

            var reportData = new ReportData
            {
                ReportLocation = "/Reports/InvoiceReport.rdlc",
                DataSourceName = "ServicesToProvide",
                DataSourceValue = lastIssuedInvoice.ServicesToProvide,
                ReportParameters = reportParams
            };

            _navigationService.ShowWindowAsModal(WindowType.ReportViewerWindow, reportData);
            base.OnArgumentPassing(new ParameterPassingEventArgs(WindowResult.Success));

            base.OnWindowCloseRequested();
        }

        private bool CanExecuteConfirmNewInvoice()
        {
            return !string.IsNullOrWhiteSpace(Customer.FirstName) &&
                !string.IsNullOrWhiteSpace(Customer.LastName) &&
                !string.IsNullOrWhiteSpace(Customer.PhoneNumber) &&
                !string.IsNullOrWhiteSpace(Customer.Address.City) &&
                !string.IsNullOrWhiteSpace(Customer.Address.Neighborhood) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.Manufacturer) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.Model) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.PlateNumber) &&
                !(ServicesToProvide.Count == 0);
        }

        private void OnEditServicesArgumentPassing(object sender, ParameterPassingEventArgs e)
        {
            var services = e.Parameter as ObservableCollection<ServiceToProvide>;
            ServicesToProvide = services;

            TotalCost = 0;
            foreach (var serviceToProvide in ServicesToProvide)
                TotalCost += serviceToProvide.ChargedPrice;
        }
    }
}
