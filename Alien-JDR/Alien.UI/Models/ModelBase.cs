using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public abstract class ModelBase
    {
        public ObservableCollection<ValidationResult> ValidationResults { get; private set; }

        //protected void OnPropertyChanged(object? value, [CallerMemberName] string property = null)
        //{
        //    ICollection<ValidationResult> validationResults = new Collection<ValidationResult>();
        //    bool result = Validator.TryValidateProperty(
        //        value,
        //        new ValidationContext(this)
        //        {
        //            MemberName = property
        //        },
        //        validationResults);

        //    if(!result)
        //    {
        //        ValidationResults.AddRange(validationResults);
        //    }
        //    OnPropertyChanged(new PropertyChangedEventArgs(property));
        //}

        protected void ValidateProperty<T>(ref T origin, T value, [CallerMemberName] string property = null)
        {
            ICollection<ValidationResult> validationResults = new Collection<ValidationResult>();
            bool result = Validator.TryValidateProperty(
                value,
                new ValidationContext(this)
                {
                    MemberName = property
                },
                validationResults);

            if (!result)
            {
                ValidationResults.AddRange(validationResults);
            }

            origin = value;
        }
    }
}
