using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class AccountValueRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void Add_ReturnsNewlyCreatedAccountValue()
        {
            // Arrange
            var accountValueRepo = new AccountValueRepository(_context);

            // Act
            var result = accountValueRepo.Add(new AccountValue
            {
                AccountValueId = 62,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Account = _context.Accounts.FirstOrDefault(x => x.AccountId == 1)
            });

            // Assert
            Assert.IsType<AccountValue>(result);
            Assert.Equal(new DateTime(2019, 2, 7), result.Date);
            Assert.Equal(1500.50m, result.Value);
            Assert.Equal(1.5445, result.RateToUserCurrency);
            Assert.Equal(2317.52m, result.ValueUserCurrency);
            Assert.Equal(1, result.Account.AccountId);
        }

        [Fact]
        public void DeleteById_GetByIdReturnsNull_GivenValidAccountValueIdToDelete()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            accountValueRepo.DeleteById(2);
            var result = accountValueRepo.GetById(2);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_ThrowsArgumentNullException_GivenInvalidAccountValueId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            Assert.Throws<ArgumentNullException>(() => accountValueRepo.DeleteById(200));
        }

        [Fact]
        public void DeleteByIds_GetByIdReturnsNull_GivenValidAccountValueIdToDelete()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            int[] accountVals = { 1, 2, 3 };
            accountValueRepo.DeleteByIds(accountVals);
            var result1 = accountValueRepo.GetById(1);
            var result2 = accountValueRepo.GetById(2);
            var result3 = accountValueRepo.GetById(3);

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }

        [Fact]
        public void DeleteByIds_ThrowsArgumentNullException_GivenInvalidAccountValueId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            int[] accountVals = { 1, 2, 300 };
            Assert.Throws<ArgumentNullException>(() => accountValueRepo.DeleteByIds(accountVals));
        }

        [Fact]
        public void GetAllForAccount_ReturnsCorrectType()
        {            
            var accountValueRepo = new AccountValueRepository(_context);
            
            var result = accountValueRepo.GetAllForAccount(1);
            
            Assert.IsAssignableFrom<IEnumerable<AccountValue>>(result);
        }

        [Fact]
        public void GetAllForAccount_ReturnsAllAccountValues()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.GetAllForAccount(2);

            var accountValues = Assert.IsAssignableFrom<IEnumerable<AccountValue>>(result);
            Assert.Equal(5, accountValues.Count());
        }

        [Fact]
        public void GetLatestForAccount_ReturnsCorrectType()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.GetLatestForAccount(1);

            Assert.IsAssignableFrom<AccountValue>(result);
        }

        [Fact]
        public void GetLatestForAccount_ReturnsAccountValue_GivenValidId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.GetLatestForAccount(2);

            var accountValue = Assert.IsAssignableFrom<AccountValue>(result);
            Assert.Equal(4006.88m, accountValue.Value);
        }

        [Fact]
        public void GetById_ReturnsAccountValue_GivenValidId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.GetById(6);

            var accountValue = Assert.IsAssignableFrom<AccountValue>(result);
            Assert.Equal(3731.21m, accountValue.Value);
        }

        [Fact]
        public void GetById_ReturnsNull_GivenInvalidId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.GetById(100);

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsAmendedAccountValue_GivenValidIds()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.Update(new AccountValue
            {
                AccountValueId = 6,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Account = _context.Accounts.FirstOrDefault(x => x.AccountId == 1)
            });

            // Assert
            Assert.IsType<AccountValue>(result);
            Assert.Equal(new DateTime(2019, 2, 7), result.Date);
            Assert.Equal(1500.50m, result.Value);
            Assert.Equal(1.5445, result.RateToUserCurrency);
            Assert.Equal(2317.52m, result.ValueUserCurrency);
            Assert.Equal(1, result.Account.AccountId);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidAccountValueId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.Update(new AccountValue
            {
                AccountValueId = 16,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Account = _context.Accounts.FirstOrDefault(x => x.AccountId == 1)
            });

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidUserId()
        {
            var accountValueRepo = new AccountValueRepository(_context);

            var result = accountValueRepo.Update(new AccountValue
            {
                AccountValueId = 6,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Account = _context.Accounts.FirstOrDefault(x => x.AccountId == 2)
            });

            Assert.Null(result);
        }

    }
}
