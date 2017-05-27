using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utility.Tests
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void Validator_GivenEmptyInput_ReturnsFalse()
        {
            var response = Validator.IsValidRoverInputCommand("");

            Assert.IsFalse(response);
        }

        [TestMethod]
        public void Validator_GivenNullInput_ReturnsFalse()
        {
            var response = Validator.IsValidRoverInputCommand(null);

            Assert.IsFalse(response);
        }

        [TestMethod]
        public void Validator_GivenWhiteSpaceInput_ReturnsFalse()
        {
            var response = Validator.IsValidRoverInputCommand(" ");

            Assert.IsFalse(response);
        }
    }
}
