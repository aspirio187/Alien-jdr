using Alien.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Alien.UI.Models
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        private ObservableCollection<ValidationResult> _validationResults = new();
        public ObservableCollection<ValidationResult> ValidationResults
        {
            get =>  CleanResults(_validationResults);
            set => _validationResults = value;
        }

        public bool IsValid
        {
            get
            {
                return Validator.TryValidateObject(this, new ValidationContext(this), ValidationResults);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                ValidationResults.AddRangeUniqueValidationResult(validationResults);
            }

            IEnumerable<ValidationResult> errors = ValidationResults.Where(x => x.MemberNames.Contains(property));
            List<ValidationResult> removable = new List<ValidationResult>();
            foreach (ValidationResult item in from ValidationResult item in errors
                                              where !validationResults.Contains(item)
                                              select item)
            {
                removable.Add(item);
            }

            ValidationResults.RemoveRange(removable);
            origin = value;
        }

        protected ObservableCollection<ValidationResult> CleanResults(ObservableCollection<ValidationResult> validationResults)
        {
            for (int i = 0; i < validationResults.Count; i++)
            {
                if(validationResults.Any(v => v.ErrorMessage.Equals(validationResults[i].ErrorMessage)))
                {
                    validationResults.RemoveAt(i);
                    i--;
                }
            }

            return validationResults;
        }
    }
}
