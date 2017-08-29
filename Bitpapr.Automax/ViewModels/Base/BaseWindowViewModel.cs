using Bitpapr.Automax.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ViewModels
{
    /// <summary>
    /// Base class for all view models that windows use as data context
    /// </summary>
    public abstract class BaseWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Event raised when this view model want to pass parameters
        /// to outside entities, such as other view models.
        /// </summary>
        public event EventHandler<ParameterPassingEventArgs> ParameterPassing;

        /// <summary>
        /// Event raised to notify that we want to close the window
        /// </summary>
        public event EventHandler WindowCloseRequested;

        /// <summary>
        /// Raise <see cref="WindowCloseRequested" event/>
        /// </summary>
        protected virtual void OnWindowCloseRequested() =>
            WindowCloseRequested?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Raise <see cref="ParameterPassing" event/>
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnArgumentPassing(ParameterPassingEventArgs args) =>
            ParameterPassing?.Invoke(this, args);
    }
}
