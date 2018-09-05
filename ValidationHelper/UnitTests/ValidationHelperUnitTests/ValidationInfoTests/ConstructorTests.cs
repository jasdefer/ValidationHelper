using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidationHelper;

namespace ValidationHelperUnitTests.ValidationInfoTests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void CreateSuccess()
        {
            var info = new ValidationInfo();
            Assert.AreEqual(true, info.IsValid, "The default constructor is wrongly invalid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(0, info.Errors.Count, $"The valid ValidationInfo contains {info.Errors.Count} error(s).");
        }

        [TestMethod]
        public void StaticCreateSuccess()
        {
            var info = ValidationInfo.CreateSuccess;
            Assert.AreEqual(true, info.IsValid, "The static method to create a success is wrongly invalid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(0, info.Errors.Count, $"The valid ValidationInfo contains {info.Errors.Count} error(s).");
        }

        [DataTestMethod]
        [DataRow("error")]
        [DataRow("Err0r5!!.,")]
        public void CreateError(string error)
        {
            var info = new ValidationInfo(error);
            Assert.AreEqual(false, info.IsValid, "The constructor to create a info with a single error is wrongly valid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(1, info.Errors.Count, $"The ValidationInfo created with a single error contains {info.Errors.Count} errors.");
            Assert.AreEqual(error, info.Errors[0], $"The single error message is {info.Errors[0]} instead of {error}.");
        }

        [DataTestMethod]
        [DataRow("error")]
        [DataRow("Err0r5!!.,")]
        public void StaticCreateError(string error)
        {
            var info = ValidationInfo.CreateFailure(error);
            Assert.AreEqual(false, info.IsValid, "The constructor to create a info with a single error is wrongly valid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(1, info.Errors.Count, $"The ValidationInfo created with a single error contains {info.Errors.Count} errors.");
            Assert.AreEqual(error, info.Errors[0], $"The single error message is {info.Errors[0]} instead of {error}.");
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void CreateErrors(int errorCount)
        {
            string[] errors = new string[errorCount];
            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = $"error{i + 1}";
            }

            var info = new ValidationInfo(errors);
            Assert.AreEqual(false, info.IsValid, "The constructor to create a info with a single error is wrongly valid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(errorCount, info.Errors.Count, $"The ValidationInfo created with a {errorCount} errors contains {info.Errors.Count} error(s).");
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void StaticCreateErrors(int errorCount)
        {
            string[] errors = new string[errorCount];
            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = $"error{i + 1}";
            }

            var info = ValidationInfo.CreateFailure(errors);
            Assert.AreEqual(false, info.IsValid, "The constructor to create a info with a single error is wrongly valid.");
            Assert.IsNotNull(info.Errors, "The error list of the ValidationInfo is wrongly null.");
            Assert.AreEqual(errorCount, info.Errors.Count, $"The ValidationInfo created with a {errorCount} errors contains {info.Errors.Count} error(s).");
        }
    }
}