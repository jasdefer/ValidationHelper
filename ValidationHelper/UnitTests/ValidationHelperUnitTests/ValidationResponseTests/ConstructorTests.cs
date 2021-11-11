namespace ValidationHelperUnitTests.ValidationResponseTests;

[TestClass]
public class ConstructorTests
{
    [TestMethod]
    public void CreateIntSuccess()
    {
        var result = new ValidationResponse<int>(1);
        Assert.AreEqual(true, result.IsValid, "The constructor with a provided value is wrongly invalid.");
        Assert.IsNotNull(result.Value, "The value is wrongly null.");
        Assert.AreEqual(1, result.Value, "The value is wrongly stored.");
    }

    [DataTestMethod]
    [DataRow("response")]
    public void CreateStringSuccess(string response)
    {
        var result = new ValidationResponse<string>(response);
        Assert.AreEqual(true, result.IsValid, "The constructor with a provided value is wrongly invalid.");
        Assert.IsNotNull(result.Value, "The value is wrongly null.");
        Assert.AreEqual(response, result.Value, "The value is wrongly stored.");
    }

    [TestMethod]
    public void StaticCreateIntSuccess()
    {
        var result = new ValidationResponse<int>(1);
        Assert.AreEqual(true, result.IsValid, "Creating a success with a provided value is wrongly invalid.");
        Assert.IsNotNull(result.Value, "The value is wrongly null.");
        Assert.AreEqual(1, result.Value, "The value is wrongly stored.");
    }

    [DataTestMethod]
    [DataRow("response")]
    public void StaticCreateStringSuccess(string response)
    {
        var result = new ValidationResponse<string>(response);
        Assert.AreEqual(true, result.IsValid, "Creating a success with a provided value is wrongly invalid.");
        Assert.IsNotNull(result.Value, "The value is wrongly null.");
        Assert.AreEqual(response, result.Value, "The value is wrongly stored.");
    }

    [TestMethod]
    public void CreateFailureWithValue()
    {
        var result = new ValidationResponse<int>(1);
        result.AddError("error");
        Assert.AreEqual(false, result.IsValid, "A ValidationResponse with a value and an error message is wrongly valid.");
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    public void CreateFailure()
    {
        var result = ValidationResponse<int>.CreateFailure("error");
        Assert.AreEqual(false, result.IsValid, "Creating a failure delivers a valid ValidationResponse.");
        Assert.IsNotNull(result.Errors, "The error list of the ValidationInfo is wrongly null.");
        Assert.AreEqual(1, result.Errors.Count, $"The ValidationInfo created with one error contains {result.Errors.Count} error(s).");
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(10)]
    [DataRow(100)]
    public void CreateFailureMultipleErrors(int errorCount)
    {
        string[] errors = new string[errorCount];
        for (int i = 0; i < errors.Length; i++)
        {
            errors[i] = $"error{i + 1}";
        }
        var result = ValidationResponse<int>.CreateFailure(errors);
        Assert.AreEqual(false, result.IsValid, "Creating a failure delivers a valid ValidationResponse.");
        Assert.IsNotNull(result.Errors, "The error list of the ValidationInfo is wrongly null.");
        Assert.AreEqual(errorCount, result.Errors.Count, $"The ValidationInfo created with {errorCount} error(s) contains {result.Errors.Count} error(s).");
    }
}
