using System;
using Bitpapr.Automax.ViewModels;

namespace Bitpapr.Automax.Navigation
{
    /// <summary>
    /// Interface for implementers that want to enable navigation in a
    /// decoupled way
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Open the specified window and optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="parameter">The parameter to pass to the windows</param>
        void ShowWindow(WindowType windowToNavigateTo, object parameter = null);

        /// <summary>
        /// Open the specified window as modal and optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="parameter">The parameter to pass to the windows</param>
        void ShowWindowAsModal(WindowType windowToNavigateTo, object parameter = null);

        /// <summary>
        /// Open the specified window as modal, and listen for any results from
        /// the window. Optionally pass arguments to it.
        /// </summary>
        /// <param name="windowToNavigateTo">The window to show</param>
        /// <param name="onArgumentPassing">The delegate called when there are results available</param>
        /// <param name="parameter">The parameter to pass to the windows</param>
        void ShowWindowAsModalForResult(WindowType windowToNavigateTo,
            EventHandler<ParameterPassingEventArgs> onArgumentPassing,
            object parameter = null);
    }
}