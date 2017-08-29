using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Services;
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

        public decimal TotalCost { get; set; }

        public ObservableCollection<ServiceToProvide> ServicesToProvide { get; set; }

        public ICommand CancelCommand { get; set; }
        public ICommand EditServicesCommand { get; set; }

        public NewInvoiceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ServicesToProvide = new ObservableCollection<ServiceToProvide>();
            EditServicesCommand = new RelayCommand(OnEditServices);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());
        }

        private void OnEditServices()
        {
            _navigationService.ShowWindowAsModalForResult(
                WindowType.EditServicesWindow,
                OnEditServicesArgumentPassing,
                ServicesToProvide);
        }

        private void OnEditServicesArgumentPassing(object sender, ParameterPassingEventArgs e)
        {
            var services = e.Parameter as ObservableCollection<ServiceToProvide>;
            if (services == null)
                return;

            ServicesToProvide = services;
        }
    }
}
