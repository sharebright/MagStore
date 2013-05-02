using System.Collections.Generic;

namespace MagStore.Infrastructure
{
    /// <summary>
    /// Provides a base class container for a client validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationRule
    {
        private readonly Dictionary<string, object> validationParameters = new Dictionary<string, object>();
        private string validationType;

        /// <summary>
        /// Gets or sets the error message for the client validation rule.
        /// </summary>
        /// 
        /// <returns>
        /// The error message for the client validation rule.
        /// </returns>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets the list of validation parameters.
        /// </summary>
        /// 
        /// <returns>
        /// A list of validation parameters.
        /// </returns>
        public IDictionary<string, object> ValidationParameters
        {
            get
            {
                return validationParameters;
            }
        }

        /// <summary>
        /// Gets or sets the validation type.
        /// </summary>
        /// 
        /// <returns>
        /// The validation type.
        /// </returns>
        public string ValidationType
        {
            get
            {
                return validationType ?? string.Empty;
            }
            set
            {
                validationType = value;
            }
        }
    }
}