using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LoginModel : ModelBase
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { ValidateProperty(ref _username, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { ValidateProperty(ref _password, value); }
        }
    }
}
