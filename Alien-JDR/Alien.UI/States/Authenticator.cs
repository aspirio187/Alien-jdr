using Alien.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.States
{
    public class Authenticator : IAuthenticator
    {
        public UserModel User { get; private set; }

        public Task<bool> LogIn(string username, string password, string rememberMe)
        {
            // TODO : Logique de connexion
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            // TODO : Logique de déconnexion
            throw new NotImplementedException();
        }

        public Task Register()
        {
            // TODO : Logique d'enregistrement
            throw new NotImplementedException();
        }
    }
}
