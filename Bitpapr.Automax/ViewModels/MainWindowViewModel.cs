using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var invoices = _invoiceService.GetLastIssuedInvoices(10);

            LastIssuedInvoices.Clear();
            foreach (Invoice invoice in invoices)
            {
                LastIssuedInvoices.Add(invoice);
            }
        }

        private void NewInvoice() => _navigationService.ShowWindowAsModal(WindowType.NewInvoiceWindow);
    }
}
