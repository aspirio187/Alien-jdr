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
    public class RegistrationViewModel : BindableBase, IDialogAware
    {
        private readonly IAuthenticator _authenticator;

        private RegistrationModel _registration = new RegistrationModel();
        public RegistrationModel Registration
        {
            get => _registration;
            set => SetProperty(ref _registration, value);
        }

        private DelegateCommand _registerAccountCommand;

        public DelegateCommand RegisterAccountCommand => _registerAccountCommand ??= new DelegateCommand(async () => await RegisterAccount());

        private DelegateCommand _navigateBackToLoginCommand;

        public DelegateCommand NavigateBackToLoginCommand => _navigateBackToLoginCommand ??= new DelegateCommand(RaiseRequestClose);

        public string Title => "Inscription";

        public event Action<IDialogResult> RequestClose;

        public RegistrationViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator ??
                throw new ArgumentNullException(nameof(authenticator));
        }

        public async Task RegisterAccount()
        {
            if(Registration.IsValid)
            {
                await _authenticator.Register(Registration);
            }
        }

        public void RaiseRequestClose()
        {
            RequestClose?.Invoke(null);
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
    }
}
