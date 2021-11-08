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

        private DelegateCommand _navigateRegistrationCommand;

        public DelegateCommand NavigateRegistrationCommand => _navigateRegistrationCommand ??= new DelegateCommand(NavigateRegister);


        public LoginViewModel(IAuthenticator authenticator, IDialogService dialogService)
        {
            _authenticator = authenticator;
            _dialogService = dialogService;
        }

        public string Title => "Connexion";

        public event Action<IDialogResult> RequestClose;

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
            // TODO : Si le login est correct continuez, sinon éteindre
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
