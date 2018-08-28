using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using Unidays.Interview.Data.Users.Queries;
using Unidays.Interview.Domain.Entities;
using Xunit;

namespace Unidays.Interview.Data.Test.Users.Queries
{
    public class UserExistsDbQueryTests
    {
        private Mock<IDbCommand> _dbCommand;
        private Mock<IDbConnection> _dbConnection;
        private UserExistsDbQuery _subject;
        private User _user;

        public UserExistsDbQueryTests()
        {
            _dbCommand = new Mock<IDbCommand>();
            _dbCommand.SetupSet(c => c.CommandText = It.IsAny<string>()).Verifiable();
            _dbCommand.Setup(c => c.ExecuteScalar()).Returns(true);
            _dbCommand.Setup(c => c.Dispose());
            _dbCommand.Setup(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()));
            _dbCommand.Setup(c => c.CreateParameter()).Returns(new SqlParameter());

            _dbConnection = new Mock<IDbConnection>();
            _dbConnection.Setup(c => c.Open());
            _dbConnection.Setup(c => c.Close());
            _dbCommand.Setup(c => c.Connection).Returns(_dbConnection.Object);

            _user = new User() { EmailAddress = "user@email.com"};

            _subject = new UserExistsDbQuery(_dbCommand.Object);
        }

        [Fact]
        public void Constructor_SetsCommandTextToCorrectValue()
        {
            var expectedSql = "EXEC DoesUserExist @emailaddress = @email";

            _dbCommand.VerifySet(c => c.CommandText = It.Is<string>(x => x == expectedSql), Times.Once);
        }

        [Fact]
        public void Execute_OpensConnection()
        {
            _subject.Execute(_user);

            _dbConnection.Verify(c => c.Open(), Times.Once);
        }

        [Fact]
        public void Execute_ExecutesCommand()
        {
            _subject.Execute(_user);

            _dbCommand.Verify(c => c.ExecuteScalar(), Times.Once);
        }

        [Fact]
        public void Execute_ClosesConnection()
        {
            _subject.Execute(_user);

            _dbConnection.Verify(c => c.Close(), Times.Once);
            _dbCommand.Verify(c => c.Dispose(), Times.Once);
        }

        [Fact]
        public void Execute_WhenEmailAddressIsEmpty_ThrowsException()
        {
            _user.EmailAddress = null;
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(_user));
        }

        [Fact]
        public void Execute_ReturnsValueFromDataQuery()
        {
            _dbCommand.Setup(c => c.ExecuteScalar()).Returns(true);
            var expectedTrue = _subject.Execute(_user);

            _dbCommand.Setup(c => c.ExecuteScalar()).Returns(false);
            var expectedFalse = _subject.Execute(_user);

            Assert.True(expectedTrue);
            Assert.False(expectedFalse);
        }

        [Fact]
        public void Execute_PreventsSQLInjection()
        {
            _subject.Execute(_user);

            _dbCommand.Verify(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()), Times.AtLeastOnce());
        }
    }
}
