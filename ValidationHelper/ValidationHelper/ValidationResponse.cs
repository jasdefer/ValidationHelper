namespace ValidationHelper;

/// <summary>
/// The ValidationResponse contains a generic property of the requested type and a collection of errors. If no errors are provided the response is valid, even if the generic property is assigned.
/// </summary>
/// <typeparam name="T">The type of the response.</typeparam>
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
}
