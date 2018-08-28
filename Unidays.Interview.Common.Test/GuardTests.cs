using System;
using Xunit;

namespace Unidays.Interview.Common.Test
{
    public class GuardTests
    {
        [Fact]
        public void NotNull_WhenNotNull_DoesNotThrowException()
        {
            string testValue = "anything";
            Guard.NotNull(testValue, "anything");
            Assert.True(true);
        }

        [Fact]
        public void NotNull_WhenNull_ThrowsArgumentNullException()
        {
            string testValue = null;
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull(testValue, "test"));
        }

        [Fact]
        public void NotNull_WhenNull_SetsArgumentNameToValueProvided()
        {
            string testValue = null;
            var objectName = "this is unrelated to the property above";

            try
            {
                Guard.NotNull(testValue, objectName);
            }
            catch (ArgumentNullException exc)
            {
                Assert.Contains(objectName, exc.Message);
            }
        }
    }
}
