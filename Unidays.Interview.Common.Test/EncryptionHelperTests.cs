using Xunit;

namespace Unidays.Interview.Common.Test
{
    public class EncryptionHelperTests
    {
        [Fact]
        public void CreateSalt_CreatesNewString()
        {
            var result = EncryptionHelpers.CreateSalt(10);

            var isResultNullOrEmpty = string.IsNullOrWhiteSpace(result);
            Assert.False(isResultNullOrEmpty);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public void CreateSalt_DoesNotCreateIdenticalString(int value)
        {
            var result = EncryptionHelpers.CreateSalt(value);

            var result2 = EncryptionHelpers.CreateSalt(value);

            Assert.NotEqual(result, result2);
        }

        [Fact]
        public void GetHashedString_ChangesStringValue()
        {
            var original = "test value";

            var result = EncryptionHelpers.GetHashedString(original, "any salt");

            Assert.NotEqual(original, result);
        }

        [Theory]
        [InlineData("testpass", "testsalt")]
        [InlineData("test", "salt")]
        [InlineData("lorem ipsum", "sit amit")]
        public void GetHashedString_WhenParametersAreTheSame_AlwaysCreatesIdenticalValue(string pass, string salt)
        {
            var result = EncryptionHelpers.GetHashedString(pass, salt);

            var result2 = EncryptionHelpers.GetHashedString(pass, salt);

            Assert.Equal(result, result2);
        }

        [Fact]
        public void GetHashedString_CreatesDifferentValuesForDifferentStrings()
        {
            var salt = "test salt";

            var result = EncryptionHelpers.GetHashedString("test 1", salt);
            var result2 = EncryptionHelpers.GetHashedString("different test", salt);

            Assert.NotEqual(result, result2);
        }

        [Fact]
        public void GetHashedString_CreatesDifferentValuesForDifferentSalts()
        {
            var value = "test value";

            var result = EncryptionHelpers.GetHashedString(value, "salt");
            var result2 = EncryptionHelpers.GetHashedString(value, "different salt");

            Assert.NotEqual(result, result2);
        }
    }
}
