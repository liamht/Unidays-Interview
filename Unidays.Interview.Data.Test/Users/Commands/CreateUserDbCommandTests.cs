using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using Unidays.Interview.Data.Users.Commands;
using Unidays.Interview.Domain.Entities;
using Xunit;

namespace Unidays.Interview.Data.Test.Users.Commands
{
    public class CreateUserDbCommandTests
    {
        private Mock<IDbCommand> _dbCommand;
        private Mock<IDbConnection> _dbConnection;
        private CreateUserDbCommand _subject;
        private User _user;

        public CreateUserDbCommandTests()
        {
            _dbCommand = new Mock<IDbCommand>();
            _dbCommand.SetupSet(c => c.CommandText = It.IsAny<string>()).Verifiable();
            _dbCommand.Setup(c => c.ExecuteNonQuery());
            _dbCommand.Setup(c => c.Dispose());
            _dbCommand.Setup(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()));
            _dbCommand.Setup(c => c.CreateParameter()).Returns(new SqlParameter());

            _dbConnection = new Mock<IDbConnection>();
            _dbConnection.Setup(c => c.Open());
            _dbConnection.Setup(c => c.Close());
            _dbCommand.Setup(c => c.Connection).Returns(_dbConnection.Object);

            _user = new User() { EmailAddress = "user@email.com", Password = "password", Salt = "salt" };

            _subject = new CreateUserDbCommand(_dbCommand.Object);
        }

        [Fact]
        public void Constructor_SetsCommandTextToCorrectValue()
        {
            var expectedSql = "EXEC CreateUser @email = @userEmail, @password = @userPassword, @salt = @userSalt";                      

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

            _dbCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
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
        public void Execute_WhenPasswordIsEmpty_ThrowsException()
        {
            _user.Password = null;
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(_user));
        }

        [Fact]
        public void Execute_WhenSaltIsEmpty_ThrowsException()
        {
            _user.Salt = null;
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(_user));
        }

        [Fact]
        public void Execute_PreventsSQLInjection()
        {
            _subject.Execute(_user);

            _dbCommand.Verify(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()), Times.AtLeastOnce());
        }
    }
}
