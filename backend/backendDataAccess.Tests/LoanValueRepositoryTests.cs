using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class LoanValueRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void Add_ReturnsNewlyCreatedLoanValue()
        {
            // Arrange
            var loanValueRepo = new LoanValueRepository(_context);

            // Act
            var result = loanValueRepo.Add(new LoanValue
            {
                LoanValueId = 65,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Loan = _context.Loans.FirstOrDefault(x => x.LoanId == 1)
            });

            // Assert
            Assert.IsType<LoanValue>(result);
            Assert.Equal(new DateTime(2019, 2, 7), result.Date);
            Assert.Equal(1500.50m, result.Value);
            Assert.Equal(1.5445, result.RateToUserCurrency);
            Assert.Equal(2317.52m, result.ValueUserCurrency);
            Assert.Equal(1, result.Loan.LoanId);
        }

        [Fact]
        public void Add_ReturnsNull_GivenAlreadyUsedLoanValueId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.Add(new LoanValue
            {
                LoanValueId = 1,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Loan = _context.Loans.FirstOrDefault(x => x.LoanId == 1)
            });

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_GetByIdReturnsNull_GivenValidLoanValueIdToDelete()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            loanValueRepo.DeleteById(2);
            var result = loanValueRepo.GetById(2);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_ThrowsArgumentNullException_GivenInvalidLoanValueId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            Assert.Throws<ArgumentNullException>(() => loanValueRepo.DeleteById(200));
        }

        [Fact]
        public void DeleteByIds_GetByIdReturnsNull_GivenValidLoanValueIdToDelete()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            int[] loanVals = { 1, 2, 3 };
            loanValueRepo.DeleteByIds(loanVals);
            var result1 = loanValueRepo.GetById(1);
            var result2 = loanValueRepo.GetById(2);
            var result3 = loanValueRepo.GetById(3);

            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }

        [Fact]
        public void DeleteByIds_ThrowsArgumentNullException_GivenInvalidLoanValueId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            int[] loanVals = { 1, 2, 300 };
            Assert.Throws<ArgumentNullException>(() => loanValueRepo.DeleteByIds(loanVals));
        }

        [Fact]
        public void GetAllForLoan_ReturnsCorrectType()
        {            
            var loanValueRepo = new LoanValueRepository(_context);
            
            var result = loanValueRepo.GetAllForLoan(1);
            
            Assert.IsAssignableFrom<IEnumerable<LoanValue>>(result);
        }

        [Fact]
        public void GetAllForLoan_ReturnsAllLoanValues()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.GetAllForLoan(2);

            var loanValues = Assert.IsAssignableFrom<IEnumerable<LoanValue>>(result);
            Assert.Equal(5, loanValues.Count());
        }

        [Fact]
        public void GetLatestForLoan_ReturnsCorrectType()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.GetLatestForLoan(1);

            Assert.IsAssignableFrom<LoanValue>(result);
        }

        [Fact]
        public void GetLatestForLoan_ReturnsLoanValue_GivenValidId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.GetLatestForLoan(2);

            var loanValue = Assert.IsAssignableFrom<LoanValue>(result);
            Assert.Equal(9883.76m, loanValue.Value);
        }

        [Fact]
        public void GetById_ReturnsLoanValue_GivenValidId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.GetById(6);

            var loanValue = Assert.IsAssignableFrom<LoanValue>(result);
            Assert.Equal(10000m, loanValue.Value);
        }

        [Fact]
        public void GetById_ReturnsNull_GivenInvalidId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.GetById(100);

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsAmendedLoanValue_GivenValidIds()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.Update(new LoanValue
            {
                LoanValueId = 5,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Loan = _context.Loans.FirstOrDefault(x => x.LoanId == 1)
            });

            // Assert
            Assert.IsType<LoanValue>(result);
            Assert.Equal(new DateTime(2019, 2, 7), result.Date);
            Assert.Equal(1500.50m, result.Value);
            Assert.Equal(1.5445, result.RateToUserCurrency);
            Assert.Equal(2317.52m, result.ValueUserCurrency);
            Assert.Equal(1, result.Loan.LoanId);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidLoanValueId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.Update(new LoanValue
            {
                LoanValueId = 16,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Loan = _context.Loans.FirstOrDefault(x => x.LoanId == 1)
            });

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidUserId()
        {
            var loanValueRepo = new LoanValueRepository(_context);

            var result = loanValueRepo.Update(new LoanValue
            {
                LoanValueId = 6,
                Date = new DateTime(2019, 2, 7),
                Value = 1500.50m,
                RateToUserCurrency = 1.5445,
                ValueUserCurrency = 2317.52m,
                Loan = _context.Loans.FirstOrDefault(x => x.LoanId == 3)
            });

            Assert.Null(result);
        }

    }
}
