using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class AccountRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void Add_ReturnsNewlyCreatedAccount()
        {
            // Arrange
            var accountRepo = new AccountRepository(_context);

            // Act
            var result = accountRepo.Add(new Account
            {
                AccountId = 13,
                AccountName = "Current A/C",
                Description = "Main current account",
                Institution = "KBC",
                IsActive = true,
                Type = "Current",
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 1)
            });

            // Assert
            Assert.IsType<Account>(result);
            Assert.Equal(13, result.AccountId);
            Assert.Equal("Current A/C", result.AccountName);
            Assert.Equal("Main current account", result.Description);
            Assert.Equal("KBC", result.Institution);
            Assert.True(result.IsActive);
            Assert.Equal("Current", result.Type);
            Assert.Equal("EUR", result.QuotedCurrency.Code);
            Assert.Equal(1, result.User.UserId);
        }

        [Fact]
        public void Delete_GetByIdReturnsNull_GivenValidAccountIdToDelete()
        {
            var accountRepo = new AccountRepository(_context);

            accountRepo.Delete(2);
            var result = accountRepo.GetById(2);
            
            Assert.Null(result);
        }

        [Fact]
        public void Delete_ThrowsArgumentNullException_GivenInvalidAccountId()
        {
            var accountRepo = new AccountRepository(_context);

            Assert.Throws<ArgumentNullException>(() => accountRepo.Delete(20));
        }

        [Fact]
        public void GetAllForUser_ReturnsCorrectType()
        {            
            var accountRepo = new AccountRepository(_context);
            
            var result = accountRepo.GetAllForUser(1);
            
            Assert.IsAssignableFrom<IEnumerable<Account>>(result);
        }

        [Fact]
        public void GetAllForUser_ReturnsAllAccounts()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.GetAllForUser(2);

            var accounts = Assert.IsAssignableFrom<IEnumerable<Account>>(result);
            Assert.Equal(3, accounts.Count());
        }

        [Fact]
        public void GetById_ReturnsAccount_GivenValidId()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.GetById(6);

            var account = Assert.IsAssignableFrom<Account>(result);
            Assert.Equal("Savings A/C", account.AccountName);
        }

        [Fact]
        public void GetById_ReturnsNull_GivenInvalidId()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.GetById(20);

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsAmendedAccount_GivenValidIds()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.Update(new Account
            {
                AccountId = 1,
                AccountName = "Savings A/C",
                Description = "Main savings account",
                Institution = "Bank of Ireland",
                IsActive = false,
                Type = "Savings",
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 1)
            });

            // Assert
            Assert.IsType<Account>(result);
            Assert.Equal(1, result.AccountId);
            Assert.Equal("Savings A/C", result.AccountName);
            Assert.Equal("Main savings account", result.Description);
            Assert.Equal("Bank of Ireland", result.Institution);
            Assert.False(result.IsActive);
            Assert.Equal("Savings", result.Type);
            Assert.Equal("EUR", result.QuotedCurrency.Code);
            Assert.Equal(1, result.User.UserId);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidAccountId()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.Update(new Account
            {
                AccountId = 20,
                AccountName = "Savings A/C",
                Description = "Main savings account",
                Institution = "Bank of Ireland",
                IsActive = false,
                Type = "Savings",
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 1)
            });

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidUserId()
        {
            var accountRepo = new AccountRepository(_context);

            var result = accountRepo.Update(new Account
            {
                AccountId = 1,
                AccountName = "Savings A/C",
                Description = "Main savings account",
                Institution = "Bank of Ireland",
                IsActive = false,
                Type = "Savings",
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 3)
            });

            Assert.Null(result);
        }

    }
}
