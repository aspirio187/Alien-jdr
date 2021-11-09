using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<bool> LogIn(LoginModel loginModel)
        {
            UserSignInDto userSignIn = new UserSignInDto()
            {
                Username = loginModel.Username,
                Password = loginModel.Password
            };

            UserDto userFromRepo = await _userService.SignInAsync(userSignIn);
            User = new UserModel()
            {
                Id = userFromRepo.Id,
                Username = userFromRepo.Username,
                ConnectedAt = DateTimeOffset.Now,
                ExpiresAt = DateTimeOffset.Now.AddDays(15)
            };
            using (StreamWriter sw = new StreamWriter(Global.SESSION_PATH))
            {
                string json = JsonConvert.SerializeObject(User);
                await sw.WriteAsync(json);
            }
            return true;
        }

        public void LogOut()
        {
            // Supprimer le fichier json
            User = null;
        }

        public async Task<bool> Register(RegistrationModel registrationModel)
        {
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
