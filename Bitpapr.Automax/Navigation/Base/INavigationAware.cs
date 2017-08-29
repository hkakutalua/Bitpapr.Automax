using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Navigation
{
    /// <summary>
    /// Interface for implementers that support being navigated to
    /// </summary>
    public interface INavigationAware
    {
        /// <summary>
        /// Called when a navigation to implementer is done
        /// </summary>
        /// <param name="parameter">The parameter passed when navigating to the implementer</param>
        void NavigatedTo(object parameter);
    }
}
