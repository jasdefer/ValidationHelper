namespace ValidationHelperUnitTests.ValidationInfoTests;

[TestClass]
public class WarningTests
{
    [TestMethod]
    public void AddWarningToValid()
    {
        var info = ValidationInfo.CreateSuccess;
        info.AddWarning("warning");
        Assert.IsTrue(info.IsValid);
        Assert.IsNotNull(info.Warnings);
        Assert.AreEqual(1, info.Warnings.Count);
        Assert.AreEqual("warning", info.Warnings.ElementAt(0));
    }

    [TestMethod]
    public void AddWarningToInvalid()
    {
        var info = ValidationInfo.CreateFailure("error");
        info.AddWarning("warning");
        Assert.IsFalse(info.IsValid);
        Assert.IsNotNull(info.Warnings);
        Assert.AreEqual(1, info.Warnings.Count);
        Assert.AreEqual("warning", info.Warnings.ElementAt(0));
    }

    [TestMethod]
    public void AddMultipleWarningsToValid()
    {
        var info = ValidationInfo.CreateSuccess;
        string[] warnings = new string[]
        {
                "warning1",
                "warning2"
        };
        info.AddWarnings(warnings);
        Assert.IsTrue(info.IsValid);
        Assert.IsNotNull(info.Warnings);
        Assert.AreEqual(2, info.Warnings.Count);
        Assert.AreEqual("warning1", info.Warnings.ElementAt(0));
        Assert.AreEqual("warning2", info.Warnings.ElementAt(1));
    }

    [TestMethod]
    public void AddMultipleWarningsToInvalid()
    {
        var info = ValidationInfo.CreateFailure("error");
        string[] warnings = new string[]
        {
                "warning1",
                "warning2"
        };
        info.AddWarnings(warnings);
        Assert.IsFalse(info.IsValid);
        Assert.IsNotNull(info.Warnings);
        Assert.AreEqual(2, info.Warnings.Count);
        Assert.AreEqual("warning1", info.Warnings.ElementAt(0));
        Assert.AreEqual("warning2", info.Warnings.ElementAt(1));
    }
}
