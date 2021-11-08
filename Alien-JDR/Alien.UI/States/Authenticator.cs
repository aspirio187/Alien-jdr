using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
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
        private readonly IUserService _userService;

        public Authenticator(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

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

        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            // TODO : Logique d'enregistrement
            UserSignUpDto userSignUp = new UserSignUpDto()
            {
                Email = registrationModel.Email,
                Firstname = registrationModel.FirstName,
                Lastname = registrationModel.LastName,
                Password = registrationModel.Password,
                Username = registrationModel.Username
            };
            return _userService.SignUp(userSignUp);
        }
    }
}
