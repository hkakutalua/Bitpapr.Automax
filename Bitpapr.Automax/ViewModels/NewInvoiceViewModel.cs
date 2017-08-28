using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class NewInvoiceViewModel : BaseWindowViewModel
    {
        private readonly NavigationService _navigationService;

        public decimal TotalCost { get; set; }

        public ICommand CancelCommand { get; set; }
        public ICommand EditServicesCommand { get; set; }

        public NewInvoiceViewModel()
            : this(new NavigationService())
        {
        }

        public NewInvoiceViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            EditServicesCommand = new RelayCommand(OnEditServices);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());
        }

        private void OnEditServices()
        {
            _navigationService.ShowWindowAsModalForResult(
                WindowType.EditServicesWindow,
                OnEditServicesArgumentPassing);
        }

        private void OnEditServicesArgumentPassing(object sender, ArgumentPassingEventArgs e)
        {
            // TODO: Update services to provide collection
            Debug.WriteLine($"{sender} {e.Argument}");
        }
    }
}
