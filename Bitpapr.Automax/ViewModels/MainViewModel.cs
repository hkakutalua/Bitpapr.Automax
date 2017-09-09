﻿using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class MainViewModel : BaseWindowViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly IInvoiceService _invoiceService;

        public int CurrentInvoiceIndex { get; set; } = -1;
        public ObservableCollection<Invoice> LastIssuedInvoices { get; set; }

        public ICommand GetLastInvoicesCommand { get; set; }
        public ICommand NewInvoiceCommand { get; set; }
        public ICommand VisualizeInvoiceCommand { get; set; }

        public MainViewModel(IInvoiceService invoiceService, ILoginService loginService,
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _invoiceService = invoiceService;

            LastIssuedInvoices = new ObservableCollection<Invoice>();
            GetLastInvoicesCommand = new RelayCommand(GetLastInvoices);
            NewInvoiceCommand = new RelayCommand(NewInvoice);
            VisualizeInvoiceCommand = new RelayCommand(ExecuteVisualizeInvoice, CanVisualizeInvoice);

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

        private void ExecuteVisualizeInvoice()
        {
            _navigationService.ShowWindowAsModal(WindowType.VisualizeInvoiceWindow,
                LastIssuedInvoices[CurrentInvoiceIndex]);
        }

        private bool CanVisualizeInvoice() => !(CurrentInvoiceIndex == -1);
    }
}