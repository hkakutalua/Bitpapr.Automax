using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ViewModels
{
    /// <summary>
    /// Base class for all view models that want to notify window requestes
    /// to the window (or windows) that had assigned them as a data context 
    /// </summary>
    public abstract class BaseWindowViewModel : BaseViewModel, IWindowRequestsBroadcaster
    {
        /// <summary>
        /// Notify that we want to close the window
        /// </summary>
        public event EventHandler WindowCloseRequested;

        /// <summary>
        /// Raise <see cref="WindowCloseRequested" event/>
        /// </summary>
        protected virtual void OnWindowCloseRequested()
        {
            WindowCloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
