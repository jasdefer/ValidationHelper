using System.Collections.Generic;

namespace ValidationHelper
{
    /// <summary>
    /// The ValidationInfo contains collection of errors. If no errors are provided the reponse is valid.
    /// </summary>
    public class ValidationInfo
    {
        private readonly List<string> errors = new();
        private readonly List<string> warnings = new();
        private readonly List<string> infos = new();
        private readonly List<string> successes = new();

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
            errors.Add(error);
        }

        /// <summary>
        /// Create a new ValidationInfo. It indicates a failure with the provided errors.
        /// </summary>
        /// <param name="errors">The errors with which the ValidationInfo is created.</param>
        public ValidationInfo(IEnumerable<string> errors)
        {
            this.errors.AddRange(errors);
        }

        #region Properties
        /// <summary>
        /// The collection of all errors
        /// </summary>
        public IReadOnlyCollection<string> Errors => errors;

        /// <summary>
        /// A collection of success messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some success messages.
        /// </summary>
        public IReadOnlyCollection<string> Successes => successes;

        /// <summary>
        /// A collection of information messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some info messages.
        /// </summary>
        public IReadOnlyCollection<string> Infos => infos;

        /// <summary>
        /// A collection of warning messages.
        /// As long as there is at least one error the ValidationInfo remain invalid.
        /// Even if it has some warning messages.
        /// If there is no error but one or more warnings, the ValidationInfo is valid.
        /// </summary>
        public IReadOnlyCollection<string> Warnings => warnings;

        /// <summary>
        /// Is true, if there are no errors and false otherwise
        /// </summary>
        public virtual bool IsValid => Errors.Count == 0;
        #endregion

        #region AddMessage
        /// <summary>
        /// Add an error to the ValidationInfo.
        /// </summary>
        /// <param name="error">The error which is added to the ValidationInfo.</param>
        public void AddError(string error)
        {
            this.errors.Add(error);
        }

        /// <summary>
        /// Add a success message to the validation info.
        /// </summary>
        public void AddSuccess(string success)
        {
            successes.Add(success);
        }

        /// <summary>
        /// Add an information message to the ValidationInfo.
        /// </summary>
        public void AddInfo(string info)
        {
            infos.Add(info);
        }

        /// <summary>
        /// Add a warning message to the ValidationInfo.
        /// </summary>
        public void AddWarning(string warning)
        {
            warnings.Add(warning);
        }

        /// <summary>
        /// Add errors to the ValidationInfo.
        /// </summary>
        /// <param name="errors">The errors which are added to the ValidationInfo.</param>
        public void AddErrors(IEnumerable<string> errors)
        {
            this.errors.AddRange(errors);
        }

        /// <summary>
        /// Add multiple success messages.
        /// </summary>
        public void AddSuccess(IEnumerable<string> successes)
        {
            this.successes.AddRange(successes);
        }

        /// <summary>
        /// Add multiple information messages to the ValidationInfo.
        /// </summary>
        public void AddInfo(IEnumerable<string> infos)
        {
            this.infos.AddRange(infos);
        }

        /// <summary>
        /// Add multiple warning messages to the ValidationInfo.
        /// </summary>
        public void AddWarnings(IEnumerable<string> warnings)
        {
            this.warnings.AddRange(warnings);
        }
        #endregion

        /// <summary>
        /// Get all error, warning, info and success messages and add the to this ValidationInfo.
        /// </summary>
        /// <param name="info">This will not change.</param>
        public void Assimilate(ValidationInfo info)
        {
            if (info == null) return;
            AddErrors(info.Errors);
            AddSuccess(info.Successes);
            AddWarnings(info.Warnings);
            AddInfo(info.Infos);
        }

        /// <summary>
        /// Get a single string containing all errors. The errors are separated with the provided separation string.
        /// </summary>
        /// <param name="separator">The string which separates the errors.</param>
        public string JoinErrors(string separator = "; ")
        {
            return string.Join(separator, Errors);
        }

        /// <summary>
        /// Append a value to this validation info and convert it to a <see cref="ValidationResponse{T}"/>
        /// All warnings, infos, errors and successes are copied.
        /// </summary>
        /// <typeparam name="T">The generic type of the <see cref="ValidationResponse{T}"/></typeparam>
        /// <param name="value">The value of the <see cref="ValidationResponse{T}"/> </param>
        /// <returns>Returns a new <see cref="ValidationResponse{T}"/></returns>
        public ValidationResponse<T> ToValidationResponse<T>(T value)
        {
            var validationResponse = new ValidationResponse<T>(value);
            validationResponse.Assimilate(this);
            return validationResponse;
        }

        /// <summary>
        /// Append a value to this validation info and convert it to a <see cref="ValidationResponse{T}"/>
        /// All warnings, infos, errors and successes are copied.
        /// </summary>
        /// <typeparam name="T">The generic type of the <see cref="ValidationResponse{T}"/></typeparam>
        /// <returns>Returns a new <see cref="ValidationResponse{T}"/> without a value.</returns>
        public ValidationResponse<T> ToValidationResponse<T>()
        {
            var validationResponse = new ValidationResponse<T>();
            validationResponse.Assimilate(this);
            return validationResponse;
        }

        #region StaticCreation
        /// <summary>
        /// Create a new ValidationInfo without any messages.
        /// </summary>
        public static ValidationInfo CreateSuccess => new();

        /// <summary>
        /// Create a new ValidationInfo with a single error.
        /// </summary>
        /// <param name="error">The single error used for the creation of a new ValidationInfo.</param>
        /// <returns>Returns a new ValidationInfo with a single error.</returns>
        public static ValidationInfo CreateFailure(string error) => new(error);

        /// <summary>
        /// Create a new ValidationInfo with a collection of errors.
        /// </summary>
        /// <param name="errors">The collection of errors used for the creation of a new ValidationInfo.</param>
        /// <returns>Returns a new ValidationInfo with a collection of errors.</returns>
        public static ValidationInfo CreateFailure(IEnumerable<string> errors) => new(errors);

        /// <summary>
        /// TakeCreate a new ValidationInfo with all error, warning, info and success messages from a ValidationResponse.
        /// The value of the 
        /// </summary>
        /// <typeparam name="T">The generic type of the ValidationResponse.</typeparam>
        /// <param name="response">The messages of this response will be copied to a new ValidationInfo.</param>
        /// <returns>Returns a new ValidationInfo with the messages of the given ValidationResponse.</returns>
        public static ValidationInfo FromResponse<T>(ValidationResponse<T> response)
        {
            var info = new ValidationInfo();
            info.Assimilate(response);
            return info;
        }
        #endregion
    }
}