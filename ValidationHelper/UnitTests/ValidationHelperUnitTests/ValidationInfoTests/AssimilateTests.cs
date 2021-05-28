using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            Assert.AreEqual("error", info.Errors.ElementAt(0));
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
            Assert.AreEqual("error", info.Errors.ElementAt(0));
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
            Assert.AreEqual("warning1", info.Warnings.ElementAt(0));
        }

        [TestMethod]
        public void AssimilateNull()
        {
            var info = ValidationInfo.CreateSuccess;
            info.Assimilate(null);
            Assert.IsTrue(info.IsValid);
            Assert.AreEqual(0, info.Errors.Count);
            Assert.AreEqual(0, info.Warnings.Count);
            Assert.AreEqual(0, info.Infos.Count);
            Assert.AreEqual(0, info.Successes.Count);
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
            Assert.AreEqual("error1", info.Errors.ElementAt(0));
            Assert.AreEqual("error2", info.Errors.ElementAt(1));

            Assert.AreEqual(2, info.Warnings.Count);
            Assert.AreEqual("warning1", info.Warnings.ElementAt(0));
            Assert.AreEqual("warning2", info.Warnings.ElementAt(1));

            Assert.AreEqual(2, info.Infos.Count);
            Assert.AreEqual("info1", info.Infos.ElementAt(0));
            Assert.AreEqual("info2", info.Infos.ElementAt(1));

            Assert.AreEqual(2, info.Successes.Count);
            Assert.AreEqual("success1", info.Successes.ElementAt(0));
            Assert.AreEqual("success2", info.Successes.ElementAt(1));
        }

        [TestMethod]
        public void ToValidationResponse()
        {
            var info = new ValidationInfo();
            info.AddError("error");
            info.AddWarning("warning");
            info.AddSuccess("success");
            info.AddInfo("info");
            var response = info.ToValidationResponse<int>();

            Assert.IsNotNull(response);
            Assert.AreEqual("error", response.Errors.Single());
            Assert.AreEqual("warning", response.Warnings.Single());
            Assert.AreEqual("success", response.Successes.Single());
            Assert.AreEqual("info", response.Infos.Single());
        }

        [TestMethod]
        public void ToValidationResponseWithValue()
        {
            var info = new ValidationInfo();
            info.AddError("error");
            info.AddWarning("warning");
            info.AddSuccess("success");
            info.AddInfo("info");
            var response = info.ToValidationResponse<int>(3);

            Assert.IsNotNull(response);
            Assert.AreEqual("error", response.Errors.Single());
            Assert.AreEqual("warning", response.Warnings.Single());
            Assert.AreEqual("success", response.Successes.Single());
            Assert.AreEqual("info", response.Infos.Single());
            Assert.AreEqual(3, response.Value);
        }
    }
}