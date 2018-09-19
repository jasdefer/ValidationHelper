using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidationHelper;

namespace ValidationHelperUnitTests.ValidationInfoTests
{
    [TestClass]
    public class AssimilateTests
    {
        [TestMethod]
        public void AssimilateFailureAsSuccess()
        {
            var info = ValidationInfo.CreateSuccess;
            var info2 = ValidationInfo.CreateFailure("error");
            info.Assimilate(info2);
            Assert.IsFalse(info.IsValid);
            Assert.AreEqual(1, info.Errors.Count);
            Assert.AreEqual("error", info.Errors[0]);
        }

        [TestMethod]
        public void AssimilateSuccessAsFailure()
        {
            var info = ValidationInfo.CreateFailure("error");
            var info2 = ValidationInfo.CreateSuccess;
            info2.AddSuccess("success");
            info.Assimilate(info2);
            Assert.IsFalse(info.IsValid);
            Assert.AreEqual(1, info.Errors.Count);
            Assert.AreEqual("error", info.Errors[0]);
        }

        [TestMethod]
        public void AssimilateSuccessAsSuccess()
        {
            var info = ValidationInfo.CreateSuccess;
            var info2 = ValidationInfo.CreateSuccess;
            info2.AddWarning("warning1");
            info.Assimilate(info2);
            Assert.IsTrue(info.IsValid);
            Assert.AreEqual(1, info.Warnings.Count);
            Assert.AreEqual("warning1", info.Warnings[0]);
        }

        [TestMethod]
        public void AssimilateNull()
        {
            var info = ValidationInfo.CreateSuccess;
            info.Assimilate(null);
            Assert.IsTrue(info.IsValid);
            Assert.AreEqual(0, info.Errors.Count);
            Assert.AreEqual(0, info.Warnings.Count);
            Assert.AreEqual(0, info.Info.Count);
            Assert.AreEqual(0, info.Success.Count);
        }

        [TestMethod]
        public void AssimilateEverything()
        {
            var info = ValidationInfo.CreateSuccess;
            info.AddError("error1");
            info.AddWarning("warning1");
            info.AddInfo("info1");
            info.AddSuccess("success1");

            var info2 = ValidationInfo.CreateSuccess;
            info2.AddError("error2");
            info2.AddWarning("warning2");
            info2.AddInfo("info2");
            info2.AddSuccess("success2");

            info.Assimilate(info2);

            Assert.IsFalse(info.IsValid);
            Assert.AreEqual(2, info.Errors.Count);
            Assert.AreEqual("error1", info.Errors[0]);
            Assert.AreEqual("error2", info.Errors[1]);

            Assert.AreEqual(2, info.Warnings.Count);
            Assert.AreEqual("warning1", info.Warnings[0]);
            Assert.AreEqual("warning2", info.Warnings[1]);

            Assert.AreEqual(2, info.Info.Count);
            Assert.AreEqual("info1", info.Info[0]);
            Assert.AreEqual("info2", info.Info[1]);

            Assert.AreEqual(2, info.Success.Count);
            Assert.AreEqual("success1", info.Success[0]);
            Assert.AreEqual("success2", info.Success[1]);
        }
    }
}