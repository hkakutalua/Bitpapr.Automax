using System;

namespace Bitpapr.Automax.ViewModels
{
    /// <summary>
    /// Event parameter holder for windows and view models
    /// parameter passing when navigating
    /// </summary>
    public class ParameterPassingEventArgs : EventArgs
    {
        public ParameterPassingEventArgs(object parameter)
        {
            Parameter = parameter;
        }

        public object Parameter { get; }
    }
}
