using Bitpapr.Automax.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class NewInvoiceViewModel : BaseWindowViewModel
    {
        public decimal TotalCost { get; set; }

        public ICommand CancelCommand { get; set; }

        public NewInvoiceViewModel()
        {
            CancelCommand = new RelayCommand(OnCancelInvoiceCreation);
        }

        private void OnCancelInvoiceCreation() => base.OnWindowCloseRequested();
    }
}
