using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Silver
{
    public partial class Entity
    {
        protected string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            var validationResultList = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
            var isValid = Validator.TryValidateProperty(value, validationContext, validationResultList);
            return (isValid) ? null : Array.ConvertAll(validationResultList.ToArray(), output => output.ErrorMessage);
        }
    }
}
