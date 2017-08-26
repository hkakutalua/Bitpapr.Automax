using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ViewModels
{
    /// <summary>
    /// Interface for all implementers (usually view models) that want to
    /// pass requests for the windows that has assigned them as a data context
    /// </summary>
    public interface IWindowRequestsBroadcaster
    {
        /// <summary>
        /// Notify that we want to close the window
        /// </summary>
        event EventHandler WindowCloseRequested;
    }
}
