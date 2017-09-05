using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Exceptions;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
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
        private readonly IDialogService _dialogService;

        public Employee LoggedInEmployee { get; set; }
        public Customer Customer { get; set; }
        public Vehicle VehicleToRepair { get; set; }
        public decimal TotalCost { get; set; }

        public ObservableCollection<ServiceToProvide> ServicesToProvide { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand EditServicesCommand { get; set; }

        public NewInvoiceViewModel(IInvoiceService invoiceService, IDialogService dialogService,
            INavigationService navigationService)
        {
            _invoiceService = invoiceService;
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
            try
            {
                _invoiceService.AddNew(Customer, VehicleToRepair,
                ServicesToProvide.ToList());

                Invoice lastIssuedInvoice = _invoiceService.GetLastIssuedInvoice();
                var reportData = new ReportData
                {
                    ReportLocation = "/Reports/InvoiceReport.rdlc",
                    DataSourceName = "ServicesToProvide",
                    DataSourceValue = lastIssuedInvoice.ServicesToProvide,
                    ReportParameters = RetrieveReportParameters(lastIssuedInvoice)
                };

                _navigationService.ShowWindowAsModal(WindowType.ReportViewerWindow, reportData);
                base.OnArgumentPassing(new ParameterPassingEventArgs(WindowResult.Success));
            }
            catch (ServiceException exception)
            {
                _dialogService.ShowDetailedDialog("Erro!",
                    "Aconteceu um erro inesperado, por favor contacte/reporte ao desenvolvedor do aplicativo",
                    $"{exception.Message} \n {exception.InnerException?.Message} \n {exception.StackTrace}",
                    DialogType.Ok);
                base.OnArgumentPassing(new ParameterPassingEventArgs(WindowResult.Cancelled));
            }
            finally
            {
                base.OnWindowCloseRequested();
            }
        }

        private bool CanExecuteConfirmNewInvoice()
        {
            return !string.IsNullOrWhiteSpace(Customer.FirstName) &&
                !string.IsNullOrWhiteSpace(Customer.LastName) &&
                !string.IsNullOrWhiteSpace(Customer.PhoneNumber) &&
                !string.IsNullOrWhiteSpace(Customer.City) &&
                !string.IsNullOrWhiteSpace(Customer.Neighborhood) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.Manufacturer) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.Model) &&
                !string.IsNullOrWhiteSpace(VehicleToRepair.PlateNumber) &&
                !(ServicesToProvide.Count == 0);
        }

        private void OnEditServicesArgumentPassing(object sender, ParameterPassingEventArgs e)
        {
            var services = e.Parameter as ObservableCollection<ServiceToProvide>;
            if (services == null)
                return;

            ServicesToProvide = services;
        }

        private Dictionary<string, object> RetrieveReportParameters(Invoice invoice)
        {
            var dictionary = new Dictionary<string, object>();

            dictionary.Add("InvoiceNumber", invoice.Number);
            dictionary.Add("CustomerName", $"{invoice.Customer.FirstName} {invoice.Customer.LastName}");
            dictionary.Add("CustomerPhone", invoice.Customer.PhoneNumber);
            dictionary.Add("CustomerNeighborhood", invoice.Customer.Neighborhood);
            dictionary.Add("CustomerCity", invoice.Customer.City);
            dictionary.Add("VehicleBrand", invoice.VehicleToRepair.Manufacturer);
            dictionary.Add("VehicleModel", invoice.VehicleToRepair.Model);
            dictionary.Add("VehiclePlate", invoice.VehicleToRepair.PlateNumber);

            return dictionary;
        }
    }
}
