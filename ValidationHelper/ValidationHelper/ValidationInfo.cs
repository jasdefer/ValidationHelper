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
        /// A collection of success messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some success messages.
        /// </summary>
        public List<string> Success { get; set; } = new List<string>();

        /// <summary>
        /// A collection of information messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some info messages.
        /// </summary>
        public List<string> Info { get; set; } = new List<string>();

        /// <summary>
        /// A collection of warning messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some warning messages.
        /// If there is no error but one or more warnings, the ValidationInfo is valid.
        /// </summary>
        public List<string> Warnings { get; set; } = new List<string>();

        /// <summary>
        /// Is true, if there are no errors and false otherwise
        /// </summary>
        public virtual bool IsValid => Errors.Count == 0;

        /// <summary>
        /// Add an error to the ValidationInfo.
        /// </summary>
        /// <param name="error">The error which is added to the ValidationInfo.</param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Add a success message to the validation info.
        /// </summary>
        public void AddSuccess(string success)
        {
            Success.Add(success);
        }

        /// <summary>
        /// Add an information message to the ValidationInfo.
        /// </summary>
        public void AddInfo(string info)
        {
            Info.Add(info);
        }

        /// <summary>
        /// Add a warning message to the ValidationInfo.
        /// </summary>
        public void AddWarning(string warning)
        {
            Warnings.Add(warning);
        }

        /// <summary>
        /// Add errors to the ValidationInfo.
        /// </summary>
        /// <param name="errors">The errors which are added to the ValidationInfo.</param>
        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }

        /// <summary>
        /// Add multiple success messages.
        /// </summary>
        public void AddSuccess(IEnumerable<string> success)
        {
            Success.AddRange(success);
        }

        /// <summary>
        /// Add multiple information messages to the ValidationInfo.
        /// </summary>
        public void AddInfo(IEnumerable<string> info)
        {
            Info.AddRange(info);
        }

        /// <summary>
        /// Add multiple warning messages to the ValidationInfo.
        /// </summary>
        /// <param name="warning"></param>
        public void AddWarnings(IEnumerable<string> warning)
        {
            Warnings.AddRange(warning);
        }

        /// <summary>
        /// Get all error, warning, info and success messages and add the to this ValidationInfo.
        /// The <param name="info"/> will not changed.
        /// </summary>
        /// <param name="info">This will not change.</param>
        public void Assimilate(ValidationInfo info)
        {
            if (info == null) return;
            AddErrors(info.Errors);
            AddSuccess(info.Success);
            AddWarnings(info.Warnings);
            AddInfo(info.Info);
        }

        /// <summary>
        /// Get a single string containing all errors. The errors are separated with the provided separation string.
        /// </summary>
        /// <param name="separator">The string which separates the errors.</param>
        public string JoinErrors(string separator = ", ")
        {
            return string.Join(separator, Errors);
        }

        public static ValidationInfo CreateSuccess => new ValidationInfo();
        public static ValidationInfo CreateFailure(string error) => new ValidationInfo(error);
        public static ValidationInfo CreateFailure(IEnumerable<string> errors) => new ValidationInfo(errors);
        public static ValidationInfo FromResponse<T>(ValidationResponse<T> response)
        {
            ValidationInfo info = new ValidationInfo();
            if (response != null && !response.IsValid)
            {
                info.AddErrors(response.Errors);
            }
            return info;
        }
    }
}