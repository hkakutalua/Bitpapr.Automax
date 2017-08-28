using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class EditServicesViewModel : BaseWindowViewModel
    {
        public string ServiceName { get; set; }
        public decimal ChargedPrice { get; set; }
        public string Comments { get; set; }
        public ObservableCollection<ServiceToProvide> ServicesToProvide { get; set; }

        public ICommand AddServiceCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditServicesViewModel()
        {
            ServicesToProvide = new ObservableCollection<ServiceToProvide>();

            AddServiceCommand = new RelayCommand(OnAddService);
            ConfirmCommand = new RelayCommand(OnConfirmEdit);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());
        }

        private void OnAddService()
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

        private void OnConfirmEdit()
        {
            var services = new List<ServiceToProvide>();
            foreach (var service in ServicesToProvide)
                services.Add(service);
            base.OnArgumentPassing(new ArgumentPassingEventArgs(services));
            base.OnWindowCloseRequested();
        }
    }
}
