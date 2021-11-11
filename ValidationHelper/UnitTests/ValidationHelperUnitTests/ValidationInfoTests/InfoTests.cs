namespace ValidationHelperUnitTests.ValidationInfoTests;

[TestClass]
public class InfoTests
{
    [TestMethod]
    public void AddInfoToValid()
    {
        var info = ValidationInfo.CreateSuccess;
        info.AddInfo("info");
        Assert.IsTrue(info.IsValid);
        Assert.IsNotNull(info.Infos);
        Assert.AreEqual(1, info.Infos.Count);
        Assert.AreEqual("info", info.Infos.ElementAt(0));
    }

    [TestMethod]
    public void AddInfoToInvalid()
    {
        var info = ValidationInfo.CreateFailure("error");
        info.AddInfo("info");
        Assert.IsFalse(info.IsValid);
        Assert.IsNotNull(info.Infos);
        Assert.AreEqual(1, info.Infos.Count);
        Assert.AreEqual("info", info.Infos.ElementAt(0));
    }

    [TestMethod]
    public void AddMultipleInfoToValid()
    {
        var info = ValidationInfo.CreateSuccess;
        string[] infos = new string[]
        {
                "info1",
                "info2"
        };
        info.AddInfo(infos);
        Assert.IsTrue(info.IsValid);
        Assert.IsNotNull(info.Infos);
        Assert.AreEqual(2, info.Infos.Count);
        Assert.AreEqual("info1", info.Infos.ElementAt(0));
        Assert.AreEqual("info2", info.Infos.ElementAt(1));
    }

    [TestMethod]
    public void AddMultipleInfoToInvalid()
    {
        var info = ValidationInfo.CreateFailure("error");
        string[] infos = new string[]
        {
                "info1",
                "info2"
        };
        info.AddInfo(infos);
        Assert.IsFalse(info.IsValid);
        Assert.IsNotNull(info.Infos);
        Assert.AreEqual(2, info.Infos.Count);
        Assert.AreEqual("info1", info.Infos.ElementAt(0));
        Assert.AreEqual("info2", info.Infos.ElementAt(1));
    }
}
