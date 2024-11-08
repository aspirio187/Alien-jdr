﻿using System;
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
        [MaxLength(50, ErrorMessage = "La nom d'utilisateur doit faire au plus 50 caractères!")]
        [MinLength(5, ErrorMessage = "Le nom d'utilisateur doit faire au moins 5 caractères!")]
        public string Username
        {
            get { return _username; }
            set
            {
                ValidateProperty(ref _username, value.Trim());
                NotifyPropertyChanged();
            }
        }

        private string _email;

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse email est requise!")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse email est incorrect!")]
        [MaxLength(250, ErrorMessage = "L'adresse email ne peut faire qu'au maximum 250 caractères!")]
        public string Email
        {
            get { return _email; }
            set
            {
                ValidateProperty(ref _email, value.Trim());
                NotifyPropertyChanged();
            }
        }

        private string _firstName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est requis!")]
        [MaxLength(50, ErrorMessage = "Le prénom ne peut faire qu'au maximum 50 caractères!")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateProperty(ref _firstName, value.ToString());
                NotifyPropertyChanged();
            }
        }

        private string _lastName;

        [MaxLength(50, ErrorMessage = "Le nom de famille ne peut faire qu'au maximum 50 caractères!")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValidateProperty(ref _lastName, value.ToString());
                NotifyPropertyChanged();
            }
        }

        private string _password;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis!")]
        [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_+]).{8,20}$",
            ErrorMessage = "Votre mot de passe doit contenir\n \tUne lettre minuscule\n\tUne lettre majuscule\n\tAu moins un chiffre\n\tUn caractère spécial")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged();
            }
        }

    }
}
