using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Unidays.Interview.UI.ViewModels;
using Xunit;

namespace Unidays.Interview.UI.Test.ViewModels
{
    public class UserCreationViewModelTests
    {
        private readonly Dictionary<string, object> _emailAttributes;
        private readonly Dictionary<string, object> _passwordAttributes;

        public UserCreationViewModelTests()
        {
            _emailAttributes = typeof(UserCreationViewModel)
                .GetProperty("EmailAddress")
                .GetCustomAttributes(true)
                .ToDictionary(a => a.GetType().Name, a => a);

            _passwordAttributes = typeof(UserCreationViewModel)
                .GetProperty("UnencryptedPassword")
                .GetCustomAttributes(true)
                .ToDictionary(a => a.GetType().Name, a => a);
        }

        [Fact]
        public void EmailAddress_HasEmailAttribute()
        {
            Assert.Contains(_emailAttributes, c => c.Key == "EmailAddressAttribute");
        }

        [Fact]
        public void EmailAddress_IsRequired()
        {
            Assert.Contains(_emailAttributes, c => c.Key == "RequiredAttribute");
        }

        [Fact]
        public void EmailAddress_HasSensibleDisplayName()
        {
            Assert.Contains(_emailAttributes, c => c.Key == "DisplayAttribute");

            var attribute = _emailAttributes["DisplayAttribute"] as DisplayAttribute;
            Assert.Equal("Email", attribute.Name);
        }

        [Fact]
        public void EmailAddress_HasMaxLengthOf100()
        {
            Assert.Contains(_emailAttributes, c => c.Key == "MaxLengthAttribute");

            var attribute = _emailAttributes["MaxLengthAttribute"] as MaxLengthAttribute;
            Assert.Equal(100, attribute.Length);
        }

        [Fact]
        public void UnencryptedPassword_IsRequired()
        {
            Assert.Contains(_passwordAttributes, c => c.Key == "RequiredAttribute");
        }

        [Fact]
        public void UnencryptedPassword_HasSensibleDisplayName()
        {
            Assert.Contains(_passwordAttributes, c => c.Key == "DisplayAttribute");

            var attribute = _passwordAttributes["DisplayAttribute"] as DisplayAttribute;
            Assert.Equal("Password", attribute.Name);
        }

        [Fact]
        public void UnencryptedPassword_HasPasswordDataTypeSet()
        {
            Assert.Contains(_passwordAttributes, c => c.Key == "DataTypeAttribute");

            var attribute = _passwordAttributes["DataTypeAttribute"] as DataTypeAttribute;
            Assert.Equal(DataType.Password, attribute.DataType);
        }
    }
}
