using System;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field
                    | AttributeTargets.Parameter
                    | AttributeTargets.Property, AllowMultiple = false)]
    public class StringLengthAttribute:Attribute
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string ErrorMessage { get; set; }

        public StringLengthAttribute(int maxLength) => SetProperties(maxLength);

        public StringLengthAttribute(int maxLength, string errorMessage) => SetProperties(maxLength, errorMessage: errorMessage);

        public StringLengthAttribute(int maxLength, int minLength) => SetProperties(maxLength, minLength);

        public StringLengthAttribute(int maxLength, int minLength, string errorMessage) => SetProperties(maxLength, minLength, errorMessage);

        private void SetProperties(int maxLegth, int? minLength = null, string errorMessage = "")
        {
            if (errorMessage == "")
            {
                if (minLength == null)
                {
                    ErrorMessage = "The character length for field, {0}, must not exceed {1}";
                }
                else
                {
                    ErrorMessage = "Field, {0}, cannot have a character length that is less than {2} or greater than {1}";
                }
            }
            else
            {
                ErrorMessage = errorMessage;
            }
            MaxLength = maxLegth;
            MinLength = minLength == null ? 0 : (int)minLength;
        }
    }
}
