using Alien.UI.Commands;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        private RegistrationModel _registration = new RegistrationModel();
        public RegistrationModel Registration
        {
            get => _registration;
            set
            {
                _registration = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RegisterAccountCommand { get; private set; }
        public ICommand NavigateBackToLoginCommand { get; private set; }

        public string Title => "Inscription";

        public RegistrationViewModel(IAuthenticator authenticator, IMapper mapper, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (authenticator is null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            _navigationManager = navigationManager;

            RegisterAccountCommand = new RelayCommand(async () => await RegisterAccount());
            NavigateBackToLoginCommand = new RelayCommand(RaiseRequestClose);
        }

        public async Task RegisterAccount()
        {
            if (Registration.IsValid)
            {
                if (await _authenticator.Register(Registration))
                {
                    //RaiseRequestClose();
                }
            }
        }

        public void RaiseRequestClose()
        {
            //RequestClose?.Invoke(null);
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
    }
}
