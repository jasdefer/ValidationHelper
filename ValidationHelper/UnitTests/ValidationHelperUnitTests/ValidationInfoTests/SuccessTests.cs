using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidationHelper;

namespace ValidationHelperUnitTests.ValidationInfoTests
{
    [TestClass]
    public class SuccessTests
    {
        [TestMethod]
        public void AddSuccessToValid()
        {
            var info = ValidationInfo.CreateSuccess;
            info.AddSuccess("success");
            Assert.IsTrue(info.IsValid);
            Assert.IsNotNull(info.Success);
            Assert.AreEqual(1, info.Success.Count);
            Assert.AreEqual("success", info.Success[0]);
        }

        [TestMethod]
        public void AddSuccessToInvalid()
        {
            var info = ValidationInfo.CreateFailure("error");
            info.AddSuccess("success");
            Assert.IsFalse(info.IsValid);
            Assert.IsNotNull(info.Success);
            Assert.AreEqual(1, info.Success.Count);
            Assert.AreEqual("success", info.Success[0]);
        }

        [TestMethod]
        public void AddMultipleSuccessToValid()
        {
            var info = ValidationInfo.CreateSuccess;
            string[] success = new string[]
            {
                "success1",
                "success2"
            };
            info.AddSuccess(success);
            Assert.IsTrue(info.IsValid);
            Assert.IsNotNull(info.Success);
            Assert.AreEqual(2, info.Success.Count);
            Assert.AreEqual("success1", info.Success[0]);
            Assert.AreEqual("success2", info.Success[1]);
        }

        [TestMethod]
        public void AddMultipleSuccessToInvalid()
        {
            var info = ValidationInfo.CreateFailure("error");
            string[] success = new string[]
            {
                "success1",
                "success2"
            };
            info.AddSuccess(success);
            Assert.IsFalse(info.IsValid);
            Assert.IsNotNull(info.Success);
            Assert.AreEqual(2, info.Success.Count);
            Assert.AreEqual("success1", info.Success[0]);
            Assert.AreEqual("success2", info.Success[1]);
        }
    }
}