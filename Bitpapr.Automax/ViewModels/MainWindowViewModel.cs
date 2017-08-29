using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class MainWindowViewModel : BaseWindowViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IInvoiceService _invoiceService;

        public ObservableCollection<Invoice> LastIssuedInvoices { get; set; }

        public ICommand GetLastInvoicesCommand { get; set; }
        public ICommand NewInvoiceCommand { get; set; }

        public MainWindowViewModel(IInvoiceService invoiceService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _invoiceService = invoiceService;

            LastIssuedInvoices = new ObservableCollection<Invoice>();
            GetLastInvoicesCommand = new RelayCommand(GetLastInvoices);
            NewInvoiceCommand = new RelayCommand(NewInvoice);

            GetLastInvoices();
        }

        private void GetLastInvoices()
        {
            var invoices = _invoiceService.GetLastIssuedInvoices(50);

            LastIssuedInvoices.Clear();
            foreach (Invoice invoice in invoices)
            {
                LastIssuedInvoices.Add(invoice);
            }
        }

        private void NewInvoice()
        {
            _navigationService.ShowWindowAsModalForResult(
                WindowType.NewInvoiceWindow, (s, e) =>
                {
                    if (e.Parameter is WindowResult)
                    {
                        if (((WindowResult)e.Parameter) == WindowResult.Success)
                            GetLastInvoices();
                    }
                });
        }
    }
}
