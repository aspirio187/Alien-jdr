using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.Tools.Extensions
{
    public static class CollectionExtension
    {
        public static void AddRangeUniqueValidationResult<T>(this ICollection<T> collection, IEnumerable<T> elements)
            where T : ValidationResult
        {
            foreach (var validationResult in elements)
            {
                if (!collection.Any(vr => vr.ErrorMessage.Equals(validationResult.ErrorMessage)))
                    collection.Add(validationResult);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var validationResult in elements)
            {
                if (collection.Contains(validationResult)) collection.Remove(validationResult);
            }
        }
    }
}
