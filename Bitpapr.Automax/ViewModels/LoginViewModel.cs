using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Services;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class LoginViewModel : BaseWindowViewModel
    {
        private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;

        public string LoginName { get; set; }
        public bool HasPassword { get; set; }
        public bool IncorrectLoginMessageVisible { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public LoginViewModel(ILoginService loginService, INavigationService navigationService)
        {
            _loginService = loginService;
            _navigationService = navigationService;

            LoginCommand = new RelayParameterizedCommand<IHavePassword>(ExecuteLogin, CanExecuteLogin);
            ExitCommand = new RelayCommand(ExecuteExit);
        }

        private void ExecuteLogin(IHavePassword passwordHolder)
        {
            if (_loginService.LoginEmployee(LoginName, passwordHolder.SecurePassword.Unsecure()))
            {
                IncorrectLoginMessageVisible = false;
                _navigationService.ShowWindow(WindowType.MainWindow);
                base.OnWindowCloseRequested();
                return;
            }

            IncorrectLoginMessageVisible = true;
        }

        private void ExecuteExit() => base.OnWindowCloseRequested();

        private bool CanExecuteLogin() =>
            HasPassword && !string.IsNullOrWhiteSpace(LoginName);
    }
}
