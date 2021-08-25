using System;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field
                    | AttributeTargets.Parameter
                    | AttributeTargets.Property, AllowMultiple = false)]
    public class RegularExpressionAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public string Pattern { get; set; }

        public RegularExpressionAttribute(string pattern) => SetProperties(pattern);

        public RegularExpressionAttribute(string pattern, string errorMessage) => SetProperties(pattern, errorMessage);

        private void SetProperties(string pattern, string errorMessage = "")
        {
            Pattern = pattern;
            ErrorMessage = errorMessage == ""
                ? "Field, {0}, is invalid. The value provided does not match the declared regular expressiong pattern, {1}"
                : errorMessage;
        }
    }
}
