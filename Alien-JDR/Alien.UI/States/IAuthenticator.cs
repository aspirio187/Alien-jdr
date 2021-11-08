using Alien.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.States
{
    public interface IAuthenticator
    {
        UserModel User { get; }
        Task<bool> LogIn(LoginModel loginModel);
        Task<bool> Register(RegistrationModel registrationModel);
        void LogOut();
    }
}
