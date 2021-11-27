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
        public ObservableCollection<ValidationResult> ValidationResults { get; private set; } = new();

        public bool IsValid { get => ValidationResults.Count <= 0; }

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
    }
}
