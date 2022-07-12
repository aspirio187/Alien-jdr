using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.Tools.Helpers
{
    public class ModelValidator
    {
        public ICollection<ValidationResult> Results { get; private set; }

        public ModelValidator()
        {

        }

        public bool IsValid(object model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            try
            {
                return Validator.TryValidateObject(model, new ValidationContext(model), Results, true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }
    }
}
