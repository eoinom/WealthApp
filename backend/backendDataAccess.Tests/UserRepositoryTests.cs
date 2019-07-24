using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class UserRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void Add_ReturnsNewlyCreatedUser()
        {
            // Arrange
            var userRepo = new UserRepository(_context);

            // Act
            var result = userRepo.Add(new User
            {
                UserId = 7,
                Email = "shane@gmail.com",
                Password = "pass1234",
                FirstName = "Shane",
                LastName = "Reilly",
                Country = _context.Countries.FirstOrDefault(x => x.Iso2Code == "IE"),
                NewsletterSub = true,
                DisplayCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR")
            });

            // Assert
            Assert.IsType<User>(result);
            Assert.Equal("shane@gmail.com", result.Email);
        }

        [Fact]
        public void CheckCredentials_ReturnsTrueIfCredentialsCorrect()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.CheckCredentials("smithie@gmail.com", "jackspassw0rd");

            Assert.True(result);
        }

        [Fact]
        public void CheckCredentials_ReturnsFalseIfCredentialsIncorrect()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.CheckCredentials("smithie@gmail.com", "jackspassword");

            Assert.False(result);
        }

        [Fact]
        public void GetAll_ReturnsCorrectType()
        {            
            var userRepo = new UserRepository(_context);
            
            var result = userRepo.GetAll();
            
            Assert.IsAssignableFrom<IEnumerable<User>>(result);
        }

        [Fact]
        public void GetAll_ReturnsAllUsers()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.GetAll();

            var users = Assert.IsAssignableFrom<IEnumerable<User>>(result);
            Assert.Equal(6, users.Count());
        }

        [Fact]
        public void GetById_ReturnsUser_GivenValidId()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.GetById(3);

            var user = Assert.IsAssignableFrom<User>(result);
            Assert.Equal("Maguire", user.LastName);
        }

        [Fact]
        public void GetById_ReturnsNull_GivenInvalidId()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.GetById(33);

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsUser_GivenValidCredentials()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.Login("jerrym@hotmail.com", "showmethemoney");

            var user = Assert.IsAssignableFrom<User>(result);
            Assert.Equal("Maguire", user.LastName);
        }

        [Fact]
        public void Login_ReturnsNull_GivenInvalidCredentials()
        {
            var userRepo = new UserRepository(_context);

            var result = userRepo.Login("jerrym@hotmail.com", "Showmethemoney");

            Assert.Null(result);
        }

    }
}
