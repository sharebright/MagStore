namespace MagStore.Infrastructure
{
    /// <summary>
    /// Provides a container for a string-length validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationStringLengthRule : ModelClientValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Mvc.ModelClientValidationStringLengthRule"/> class.
        /// </summary>
        /// <param name="errorMessage">The validation error message.</param><param name="minimumLength">The minimum length of the string.</param><param name="maximumLength">The maximum length of the string.</param>
        public ModelClientValidationStringLengthRule(string errorMessage, int minimumLength, int maximumLength)
        {
            ErrorMessage = errorMessage;
            ValidationType = "length";
            if (minimumLength != 0)
                ValidationParameters["min"] = minimumLength;
            if (maximumLength == int.MaxValue)
                return;
            ValidationParameters["max"] = maximumLength;
        }
    }
}
