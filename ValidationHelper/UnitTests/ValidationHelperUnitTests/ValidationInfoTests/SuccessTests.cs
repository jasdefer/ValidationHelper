using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            Assert.IsNotNull(info.Successes);
            Assert.AreEqual(1, info.Successes.Count);
            Assert.AreEqual("success", info.Successes.ElementAt(0));
        }

        [TestMethod]
        public void AddSuccessToInvalid()
        {
            var info = ValidationInfo.CreateFailure("error");
            info.AddSuccess("success");
            Assert.IsFalse(info.IsValid);
            Assert.IsNotNull(info.Successes);
            Assert.AreEqual(1, info.Successes.Count);
            Assert.AreEqual("success", info.Successes.ElementAt(0));
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
            Assert.IsNotNull(info.Successes);
            Assert.AreEqual(2, info.Successes.Count);
            Assert.AreEqual("success1", info.Successes.ElementAt(0));
            Assert.AreEqual("success2", info.Successes.ElementAt(1));
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
            Assert.IsNotNull(info.Successes);
            Assert.AreEqual(2, info.Successes.Count);
            Assert.AreEqual("success1", info.Successes.ElementAt(0));
            Assert.AreEqual("success2", info.Successes.ElementAt(1));
        }
    }
}