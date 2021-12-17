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
        private string _username;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom d'utilisateur est requis!")]
        [MaxLength(50, ErrorMessage = "La taille maximale est de 50!")]
        public string Username
        {
            get { return _username; }
            set { ValidateProperty(ref _username, value.Trim()); }
        }

        private string _email;

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse email est requise!")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse email est incorrect!")]
        [MaxLength(250, ErrorMessage = "L'adresse email ne peut faire qu'au maximum 250 caractères!")]
        public string Email
        {
            get { return _email; }
            set { ValidateProperty(ref _email, value.Trim()); }
        }

        private string _firstName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est requis!")]
        [MaxLength(50, ErrorMessage = "Le prénom ne peut faire qu'au maximum 50 caractères!")]
        public string FirstName
        {
            get { return _firstName; }
            set { ValidateProperty(ref _firstName, value.ToString()); }
        }

        [MaxLength(50, ErrorMessage = "Le nom de famille ne peut faire qu'au maximum 50 caractères!")]
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { ValidateProperty(ref _lastName, value.ToString()); }
        }

        private string _password;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis!")]
        [RegularExpression("", ErrorMessage = "Votre mot de passe doit contenir une lettre minuscule, majuscule, au moins un chiffre et un caractère spécial!")]
        public string Password
        {
            get { return _password; }
            set { ValidateProperty(ref _password, value); }
        }

    }
}
