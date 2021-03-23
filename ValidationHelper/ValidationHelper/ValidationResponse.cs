using System.Collections.Generic;

namespace ValidationHelper
{
    /// <summary>
    /// The ValidationResponse contains a generic property of the requested type and a collection of errors. If no errors are provided the response is valid, even if the generic property is assigned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationResponse<T> : ValidationInfo
    {
        /// <summary>
        /// Create a new ValidaitonResponse.
        /// It indicates success until an error is added.
        /// The value of this ValidationResponse is not initialized.
        /// </summary>
        public ValidationResponse()
        {

        }

        /// <summary>
        /// Create a new ValidationResponse with the requested object. It indicates success until an error is added.
        /// </summary>
        /// <param name="value">The requested object.</param>
        public ValidationResponse(T value)
        {
            Value = value;
        }

        /// <summary>
        /// The requested value. The ValidationResponse can be invalid even if this value is set.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Create a new ValidationResponse. It indicates success until an error is added.
        /// </summary>
        /// <param name="value">The requested object.</param>
        public new static ValidationResponse<T> CreateSuccess(T value)
        {
            return new ValidationResponse<T>(value);
        }

        /// <summary>
        /// Create a new ValidationResponse. It indicates a failure with the provided error.
        /// </summary>
        /// <param name="error">The error with which the ValidationResponse is created.</param>
        public new static ValidationResponse<T> CreateFailure(string error)
        {
            var response = new ValidationResponse<T>();
            response.AddError(error);
            return response;

        }

        /// <summary>
        /// Create a new ValidationResponse. It indicates a failure with the provided errors.
        /// </summary>
        /// <param name="errors">The errors with which the ValidationResponse is created.</param>
        public new static ValidationResponse<T> CreateFailure(IEnumerable<string> errors)
        {
            var response = new ValidationResponse<T>();
            response.AddErrors(errors);
            return response;
        }

        /// <summary>
        /// Create a new validation response with the error, warning, info and success messages of a given ValidationInfo.
        /// </summary>
        /// <param name="info">The messages of this ValidationInfo object are copied. It will not be changed.</param>
        /// <returns>Returns a new ValidationResponse without initializing the value.</returns>
        public static ValidationResponse<T> FromInfo(ValidationInfo info)
        {
            var response = new ValidationResponse<T>();
            response.Assimilate(info);
            return response;
        }
    }
}