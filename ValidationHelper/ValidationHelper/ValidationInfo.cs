using System.Collections.Generic;

namespace ValidationHelper
{
    /// <summary>
    /// The ValidationInfo contains collection of errors. If no errors are provided the reponse is valid.
    /// </summary>
    public class ValidationInfo
    {
        /// <summary>
        /// Create a new ValidationInfo. It indicates success until an error is added.
        /// </summary>
        public ValidationInfo() { }

        /// <summary>
        /// Create a new ValidationInfo. It indicates a failure with the provided error.
        /// </summary>
        /// <param name="error">The error with which the ValidationInfo is created.</param>
        public ValidationInfo(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Create a new ValidationInfo. It indicates a failure with the provided errors.
        /// </summary>
        /// <param name="errors">The errors with which the ValidationInfo is created.</param>
        public ValidationInfo(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }

        /// <summary>
        /// The collection of all errors
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Is true, if there are no errors and false otherwise
        /// </summary>
        public virtual bool IsValid => Errors.Count == 0;

        public static ValidationInfo CreateSuccess => new ValidationInfo();
        public static ValidationInfo CreateFailure(string error) => new ValidationInfo(error);
        public static ValidationInfo CreateFailure(IEnumerable<string> errors) => new ValidationInfo(errors);
    }
}
