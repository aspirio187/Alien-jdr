using Alien.UI.Commands;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        public ICommand ConnectionCommand { get; private set; }
        public ICommand NavigateRegistrationCommand { get; private set; }

        private LoginModel _login = new();

        public LoginModel Login
        {
            get { return _login; }
            set
            {
                _login = value;
                NotifyPropertyChanged();
            }
        }

        public LoginViewModel(IAuthenticator authenticator, IMapper mapper, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _navigationManager = navigationManager;

            ConnectionCommand = new RelayCommand(async () => await SignIn());
            NavigateRegistrationCommand = new RelayCommand(NavigateRegister);
        }

        public string Title => "Connexion";

        public async Task SignIn()
        {
            if (await _authenticator.LogIn(Login))
            {
                //RaiseRequestClose(new DialogResult(ButtonResult.OK));
            }
        }


        public void CloseDialog(string parameter)
        {

        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        //public void OnDialogOpened(IDialogParameters parameters)
        //{

        //}

        public void NavigateRegister()
        {
            _navigationManager.OpenDialog(nameof(RegistrationView), this);
        }
    }
}
