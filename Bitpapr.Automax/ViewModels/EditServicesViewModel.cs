using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class EditServicesViewModel : BaseWindowViewModel, INavigationAware
    {
        public string ServiceName { get; set; }
        public decimal ChargedPrice { get; set; }
        public string Comments { get; set; }
        public ObservableCollection<ServiceToProvide> ServicesToProvide { get; set; }
        public int CurrentServiceIndex { get; set; } = -1;

        public ICommand AddServiceCommand { get; set; }
        public ICommand RemoveCurrentServiceCommand { get; set; }
        public ICommand ClearServicesCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditServicesViewModel()
        {
            ServicesToProvide = new ObservableCollection<ServiceToProvide>();

            AddServiceCommand = new RelayCommand(ExecuteAddService, CanExecuteAddService);
            RemoveCurrentServiceCommand = new RelayCommand(ExecuteRemoveCurrentService,
                CanExecuteRemoveCurrentService);
            ClearServicesCommand = new RelayCommand(() => ServicesToProvide.Clear());
            ConfirmCommand = new RelayCommand(ExecuteConfirmEdit);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());
        }

        public void OnNavigatedTo(object parameter)
        {
            var services = parameter as ObservableCollection<ServiceToProvide>;
            if (services != null)
            {
                ServicesToProvide.Clear();
                foreach (var service in services)
                    ServicesToProvide.Add(service);
            }
        }

        private void ExecuteAddService()
        {
            var service = new ServiceToProvide
            {
                Name = ServiceName,
                ChargedPrice = ChargedPrice,
                Comments = Comments
            };

            ServicesToProvide.Add(service);

            ServiceName = string.Empty;
            ChargedPrice = 0;
            Comments = string.Empty;
        }

        private bool CanExecuteAddService()
        {
            return !string.IsNullOrWhiteSpace(ServiceName) &&
                   !(ChargedPrice < 1);
        }

        private void ExecuteRemoveCurrentService() =>
            ServicesToProvide.RemoveAt(CurrentServiceIndex);

        private bool CanExecuteRemoveCurrentService() => !(CurrentServiceIndex == -1);

        private void ExecuteConfirmEdit()
        {
            base.OnArgumentPassing(new ParameterPassingEventArgs(ServicesToProvide));
            base.OnWindowCloseRequested();
        }
    }
}
