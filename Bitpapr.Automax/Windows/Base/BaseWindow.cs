using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.ViewModels;
using Bitpapr.Automax.InversionOfControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax
{
    /// <summary>
    /// Base class for all windows with support of parameters that can be
    /// passed to the window in the occurrence of a navigation.
    /// This base class can also pass back parameters via events to
    /// subscribers.
    /// </summary>
    public abstract class BaseWindow : Window
    {
        /// <summary>
        /// Sets or gets the parameter of this window
        /// </summary>
        public abstract object NavigationParameter { get; set; }

        /// <summary>
        /// Event raised when this window want to pass parameters
        /// to outside entities. Usually the sender of this event
        /// would the view model of this window
        /// </summary>
        public event EventHandler<ParameterPassingEventArgs> ParameterPassing;

        protected virtual void OnParameterPassing(object sender, ParameterPassingEventArgs args) =>
            ParameterPassing?.Invoke(sender, args);
    }

    /// <summary>
    /// Base class for all windows that support view models as data context
    /// </summary>
    public class BaseWindow<TViewModel> : BaseWindow
        where TViewModel : BaseWindowViewModel
    {
        private TViewModel _viewModel;
        private object _navigationParameter;

        public TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                // Unsubscribe from this event if subscription
                // was done before
                if (_viewModel != null)
                    _viewModel.ParameterPassing -= OnViewModelArgumentPassing;

                _viewModel = value;
                DataContext = value;

                // Just re-route the view model event to this class event
                _viewModel.ParameterPassing += OnViewModelArgumentPassing;
            }
        }

        /// <summary>
        /// Gets or set the parameter of this window and pass
        /// them on the view model if it's navigation aware
        /// </summary>
        public override object NavigationParameter
        {
            get => _navigationParameter;
            set
            {
                _navigationParameter = value;
                if (_viewModel is INavigationAware)
                    ((INavigationAware)_viewModel).NavigatedTo(value);
            }
        }
        
        public BaseWindow()
        {
            ViewModel = IoC.Resolve<TViewModel>();
            ViewModel.WindowCloseRequested += (s, e) => this.Close();
        }

        /// <summary>
        /// Re-route view model argument passed event to base class argument passed event/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnViewModelArgumentPassing(object sender, ParameterPassingEventArgs args) =>
            base.OnParameterPassing(sender, args);
    }
}
