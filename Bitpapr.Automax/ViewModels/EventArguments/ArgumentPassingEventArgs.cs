using System;

namespace Bitpapr.Automax.ViewModels
{
    /// <summary>
    /// Event arguments holder for windows and view models
    /// arguments passing when navigating
    /// </summary>
    public class ArgumentPassingEventArgs : EventArgs
    {
        public ArgumentPassingEventArgs(object argument)
        {
            Argument = argument;
        }

        public object Argument { get; }
    }
}
