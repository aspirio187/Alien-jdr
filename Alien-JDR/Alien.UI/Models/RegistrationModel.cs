using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class RegistrationModel : ModelBase
    {
        private string _email;

        public string Email
        {
            get { return _email; }
            set { ValidateProperty(ref _email, value); }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { ValidateProperty(ref _firstName, value); }
        }
    }
}
