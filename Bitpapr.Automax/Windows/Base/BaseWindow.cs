using Bitpapr.Automax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax
{
    /// <summary>
    /// Base class for all windows that support view models as data context
    /// </summary>
    public class BaseWindow<TViewModel> : Window
        where TViewModel : BaseWindowViewModel, new()
    {
        private TViewModel _viewModel;

        public TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
            }
        }

        public BaseWindow()
        {
            ViewModel = new TViewModel();
            ViewModel.WindowCloseRequested += (s, e) => this.Close();
        }
    }
}
