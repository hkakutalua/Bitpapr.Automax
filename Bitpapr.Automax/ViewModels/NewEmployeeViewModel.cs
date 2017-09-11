using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class NewEmployeeViewModel : BaseWindowViewModel
    {
        private readonly IEmployeeService _employeeService;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public bool HasPassword { get; set; }
        public EmployeeRole EmployeeRole { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public NewEmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            ConfirmCommand = new RelayParameterizedCommand<IHavePassword>(ExecuteConfirm, CanExecuteConfirm);
            CancelCommand = new RelayCommand(() => base.OnWindowCloseRequested());
        }

        private void ExecuteConfirm(IHavePassword havePassword)
        {
            _employeeService.Insert(
                LoginName,
                FirstName,
                LastName,
                havePassword.SecurePassword.Unsecure(),
                EmployeeRole);

            base.OnWindowCloseRequested();
        }

        private bool CanExecuteConfirm()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(LoginName) &&
                   HasPassword;
        }
    }
}
