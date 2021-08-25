using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ValidationComponent.CustomAttributes;

namespace ValidationComponent.Validation
{
    public static class ValidationLogic
    {
        public static bool PropertyValueIsValid(Type t, string enteredValue, string elementName, out string errorMessage)
        {
            PropertyInfo prop = t.GetProperty(elementName);

            Attribute[] attributes = prop.GetCustomAttributes().ToArray();
            errorMessage = "";
            foreach (Attribute attr in attributes)
            {
                switch (attr)
                {
                    case RequiredAttribute ra:
                        if (!FieldRequiredIsValid(enteredValue))
                        {
                            errorMessage = ra.ErrorMessage;
                            errorMessage = string.Format(errorMessage, prop.Name);
                            return false;
                        }
                        break;
                    case StringLengthAttribute sla:
                        if (!FieldStringLengthIsValid(sla, enteredValue))
                        {
                            errorMessage = sla.ErrorMessage;
                            errorMessage = string.Format(errorMessage, prop.Name, sla.MaxLength, sla.MinLength);
                            return false;
                        }
                        break;
                    case RegularExpressionAttribute rea:
                        if (!FieldPatternMatchIsValid(rea, enteredValue))
                        {
                            errorMessage = rea.ErrorMessage;
                            errorMessage = string.Format(errorMessage, prop.Name, rea.Pattern);
                            return false;
                        }
                        break;
                }
            }
            return true;

        }

        private static bool FieldRequiredIsValid(string enteredValue)
        {
            if (string.IsNullOrEmpty(enteredValue)) return false;
            return true;
        }

        private static bool FieldStringLengthIsValid(StringLengthAttribute stringLengthAttribute, string enteredValue)
        {
            if (enteredValue.Length >= stringLengthAttribute.MinLength &&
                enteredValue.Length <= stringLengthAttribute.MaxLength) { return true; }
            return false;
        }

        private static bool FieldPatternMatchIsValid(RegularExpressionAttribute regexAttribute, string enteredValue)
        {
            if (Regex.IsMatch(enteredValue, regexAttribute.Pattern)) return true;
            return false;
        }
    }
}
