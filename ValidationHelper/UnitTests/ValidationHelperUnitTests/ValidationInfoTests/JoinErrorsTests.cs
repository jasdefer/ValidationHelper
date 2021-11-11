namespace ValidationHelperUnitTests.ValidationInfoTests;

[TestClass]
public class JoinErrorsTests
{
    [TestMethod]
    public void JoinSuccess()
    {
        ValidationInfo info = new ValidationInfo();
        Assert.AreEqual(string.Empty, info.JoinErrors(), "Joining the errors failed for a success.");
    }

    [DataTestMethod]
    [DataRow("error")]
    [DataRow(", ")]
    [DataRow("sejrfb3j45h3j4tbnjdfgbnj546")]
    public void JoinSingleError(string error)
    {
        ValidationInfo info = ValidationInfo.CreateFailure(error);
        Assert.AreEqual(error, info.JoinErrors());
    }

    [TestMethod]
    public void JoinMultipleErrors()
    {
        string[] errors = new string[]
        {
                "error1",
                "error2"
        };

        ValidationInfo info = ValidationInfo.CreateFailure(errors);
        Assert.AreEqual("error1,error2", info.JoinErrors(","));
    }
}
