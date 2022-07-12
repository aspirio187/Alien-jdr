using Alien.UI.Models;
using Alien.UI.States;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDialogService _dialogService;

        private DelegateCommand _connectionCommand;

        public DelegateCommand ConnectionCommand => _connectionCommand ??= new DelegateCommand(async () => await SignIn());

        private DelegateCommand _navigateRegistrationCommand;

        public DelegateCommand NavigateRegistrationCommand => _navigateRegistrationCommand ??= new DelegateCommand(NavigateRegister);

        private LoginModel _login = new();

        public LoginModel Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public LoginViewModel(IAuthenticator authenticator, IDialogService dialogService)
        {
            _authenticator = authenticator;
            _dialogService = dialogService;
        }

        public string Title => "Connexion";

        public event Action<IDialogResult> RequestClose;

        public async Task SignIn()
        {
            if(await _authenticator.LogIn(Login))
            {
                RaiseRequestClose(new DialogResult(ButtonResult.OK));
            }
        }

        public void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public void CloseDialog(string parameter)
        {
            ButtonResult buttonResult = ButtonResult.None;
            RaiseRequestClose(new DialogResult(buttonResult));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public void NavigateRegister()
        {
            _dialogService.ShowDialog("RegistrationView");
        }
    }
}
