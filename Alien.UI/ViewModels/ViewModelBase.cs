using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.States;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Alien.UI.Views;

namespace Alien.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly IAuthenticator _authenticator;
        protected readonly IMapper _mapper;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
            }
        }

        protected ViewModelBase(IAuthenticator authenticator, IMapper mapper)
        {
            if (authenticator is null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _authenticator = authenticator;
            _mapper = mapper;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void OnInit()
        {
        }

        public virtual void OnNavigatedFrom(Dictionary<string, object> parameters)
        {
        }

        public virtual void OnNavigatedTo(Dictionary<string, object> parameters)
        {
        }
    }
}