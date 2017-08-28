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
    /// Base class for all windows with support of arguments that can be
    /// passed to the window in the occurrence of a navigation.
    /// This base class can also pass back arguments via events to
    /// subscribers.
    /// </summary>
    public abstract class BaseWindow : Window
    {
        /// <summary>
        /// Sets or gets the argument of this window
        /// </summary>
        public abstract object Argument { get; set; }

        /// <summary>
        /// Event raised when this window want to pass arguments
        /// to outside entities. Usually the sender of this event
        /// would the view model of this window
        /// </summary>
        public event EventHandler<ArgumentPassingEventArgs> ArgumentPassing;

        protected virtual void OnArgumentPassing(object sender, ArgumentPassingEventArgs args) =>
            ArgumentPassing?.Invoke(sender, args);
    }

    /// <summary>
    /// Base class for all windows that support view models as data context
    /// </summary>
    public class BaseWindow<TViewModel> : BaseWindow
        where TViewModel : BaseWindowViewModel, new()
    {
        private TViewModel _viewModel;

        public TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                // Unsubscribe from this event if subscription
                // was done before
                if (_viewModel != null)
                    _viewModel.ArgumentPassing -= OnViewModelArgumentPassing;

                _viewModel = value;
                DataContext = value;

                // Just re-route the view model event to this class event
                _viewModel.ArgumentPassing += OnViewModelArgumentPassing;
            }
        }

        /// <summary>
        /// Gets or set the argument of this window and pass
        /// them on the view model
        /// </summary>
        public override object Argument
        {
            get => _viewModel.Argument;
            set => _viewModel.Argument = value;
        }
        
        public BaseWindow()
        {
            ViewModel = new TViewModel();
            ViewModel.WindowCloseRequested += (s, e) => this.Close();
        }

        /// <summary>
        /// Re-route view model argument passed event to base class argument passed event/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnViewModelArgumentPassing(object sender, ArgumentPassingEventArgs args) =>
            base.OnArgumentPassing(sender, args);
    }
}
