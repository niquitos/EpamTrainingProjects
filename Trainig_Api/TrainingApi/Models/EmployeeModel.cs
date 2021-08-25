using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationComponent.Validation;

namespace TrainingApi.Models
{
    public class EmployeeModel : IValidatableObject
    {
        [ValidationComponent.CustomAttributes.RequiredAttribute]
        public int EmployeeId { get; set; }

        [ValidationComponent.CustomAttributes.RequiredAttribute]
        [ValidationComponent.CustomAttributes.StringLengthAttribute(50, 3, "Field, {0}, cannot exceed {1} characters length and cannot be less than {2} characters length.")]
        public string FirstName { get; set; }

        [ValidationComponent.CustomAttributes.RequiredAttribute]
        [ValidationComponent.CustomAttributes.StringLengthAttribute(50, 3, "Field, {0}, cannot exceed {1} characters length and cannot be less than {2} characters length.")]
        public string LastName { get; set; }

        [ValidationComponent.CustomAttributes.RequiredAttribute]
        public int Age { get; set; }

        [ValidationComponent.CustomAttributes.RequiredAttribute]
        [ValidationComponent.CustomAttributes.StringLengthAttribute(50, 3, "Field, {0}, cannot exceed {1} characters length and cannot be less than {2} characters length.")]
        [ValidationComponent.CustomAttributes.RegularExpressionAttribute(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", "The email should match the pattern: blabla@blabla.com")]
        public string EmailAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var model = (EmployeeModel)validationContext.ObjectInstance;

            var props = model.GetType().GetProperties();

            foreach (var prop in props)
            {
                var enteredValue = prop.GetValue(model);
                string message = $"The field {prop.Name} cannot be empty";

                if (enteredValue != null)
                    ValidationLogic.PropertyValueIsValid(typeof(EmployeeModel), enteredValue.ToString(), prop.Name, out message);

                yield return new ValidationResult(message);
            }
        }
    }
}
