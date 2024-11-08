using Alien.UI.States;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CreditViewModel : ViewModelBase
    {
        public CreditViewModel(IAuthenticator authenticator, IMapper mapper)
            : base(authenticator, mapper)
        {
        }
    }
}
