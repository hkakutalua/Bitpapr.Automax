using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.Commands
{
    /// <summary>
    /// Basic implementation of <see cref="ICommand"/> that supports parameters
    /// </summary>
    /// <typeparam name="T">The type of the parameter</typeparam>
    public class RelayParameterizedCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<T> _execute;
        private Func<bool> _canExecute;

        public RelayParameterizedCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayParameterizedCommand(Action<T> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        { 
            _execute?.Invoke((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute();
        }
    }
}
