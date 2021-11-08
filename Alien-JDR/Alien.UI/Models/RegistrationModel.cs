using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class RegistrationModel : ModelBase
    {
        [EmailAddress]
        private string _email;

        public string Email
        {
            get { return _email; }
            set { ValidateProperty(ref _email, value); }
        }

        private string _firstName;

        [MinLength(2)]
        public string FirstName
        {
            get { return _firstName; }
            set { ValidateProperty(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { ValidateProperty(ref _lastName, value); }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { ValidateProperty(ref _password, value); }
        }

    }
}
