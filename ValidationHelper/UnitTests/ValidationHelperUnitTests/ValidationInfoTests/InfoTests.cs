using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidationHelper;

namespace ValidationHelperUnitTests.ValidationInfoTests
{
    [TestClass]
    public class InfoTests
    {
        [TestMethod]
        public void AddInfoToValid()
        {
            var info = ValidationInfo.CreateSuccess;
            info.AddInfo("info");
            Assert.IsTrue(info.IsValid);
            Assert.IsNotNull(info.Info);
            Assert.AreEqual(1, info.Info.Count);
            Assert.AreEqual("info", info.Info[0]);
        }

        [TestMethod]
        public void AddInfoToInvalid()
        {
            var info = ValidationInfo.CreateFailure("error");
            info.AddInfo("info");
            Assert.IsFalse(info.IsValid);
            Assert.IsNotNull(info.Info);
            Assert.AreEqual(1, info.Info.Count);
            Assert.AreEqual("info", info.Info[0]);
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
            Assert.IsNotNull(info.Info);
            Assert.AreEqual(2, info.Info.Count);
            Assert.AreEqual("info1", info.Info[0]);
            Assert.AreEqual("info2", info.Info[1]);
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
            Assert.IsNotNull(info.Info);
            Assert.AreEqual(2, info.Info.Count);
            Assert.AreEqual("info1", info.Info[0]);
            Assert.AreEqual("info2", info.Info[1]);
        }
    }
}