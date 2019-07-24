using backendData.Models;
using backendDataAccess.Repositories;
using backendDataAccess.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendDataAccess.Tests
{
    public class LoanRepositoryTests : backendDataAccessTestBase
    {
        [Fact]
        public void Add_ReturnsNewlyCreatedLoan()
        {
            // Arrange
            var loanRepo = new LoanRepository(_context);

            // Act
            var result = loanRepo.Add(new Loan
            {
                LoanId = 12,
                LoanName = "My personal Loan",
                Description = "Personal loan for travelling",
                Institution = "AIB",
                IsActive = true,
                Type = "Personal Loan",
                StartPrincipal = 5000.00m,
                RateType = "Variable",
                TotalTerm = 24,
                FixedTerm = 6,
                AprRate = 0.12,
                StartDate = new DateTime(2019, 02, 10),
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 2)
            });

            // Assert
            Assert.IsType<Loan>(result);
            Assert.Equal(12, result.LoanId);
            Assert.Equal("My personal Loan", result.LoanName);
            Assert.Equal("Personal loan for travelling", result.Description);
            Assert.Equal("AIB", result.Institution);
            Assert.True(result.IsActive);
            Assert.Equal("Personal Loan", result.Type);
            Assert.Equal(5000.00m, result.StartPrincipal);
            Assert.Equal("Variable", result.RateType);
            Assert.Equal(24, result.TotalTerm);
            Assert.Equal(6, result.FixedTerm);
            Assert.Equal(0.12, result.AprRate);
            Assert.Equal(new DateTime(2019, 02, 10), result.StartDate);
            Assert.Equal("EUR", result.QuotedCurrency.Code);
            Assert.Equal(2, result.User.UserId);
        }

        [Fact]
        public void Delete_GetByIdReturnsNull_GivenValidLoanIdToDelete()
        {
            var loanRepo = new LoanRepository(_context);

            loanRepo.Delete(2);
            var result = loanRepo.GetById(2);
            
            Assert.Null(result);
        }

        [Fact]
        public void Delete_ThrowsArgumentNullException_GivenInvalidLoanId()
        {
            var loanRepo = new LoanRepository(_context);

            Assert.Throws<ArgumentNullException>(() => loanRepo.Delete(20));
        }

        [Fact]
        public void GetAllForUser_ReturnsCorrectType()
        {            
            var loanRepo = new LoanRepository(_context);
            
            var result = loanRepo.GetAllForUser(1);
            
            Assert.IsAssignableFrom<IEnumerable<Loan>>(result);
        }

        [Fact]
        public void GetAllForUser_ReturnsAllLoans()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.GetAllForUser(2);

            var loans = Assert.IsAssignableFrom<IEnumerable<Loan>>(result);
            Assert.Equal(3, loans.Count());
        }

        [Fact]
        public void GetById_ReturnsLoan_GivenValidId()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.GetById(6);

            var loan = Assert.IsAssignableFrom<Loan>(result);
            Assert.Equal("Investment Mortgage", loan.LoanName);
        }

        [Fact]
        public void GetById_ReturnsNull_GivenInvalidId()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.GetById(20);

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsAmendedLoan_GivenValidIds()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.Update(new Loan
            {
                LoanId = 10,
                LoanName = "My personal Loan",
                Description = "Personal loan for travelling",
                Institution = "AIB",
                IsActive = true,
                Type = "Personal Loan",
                StartPrincipal = 5000.00m,
                RateType = "Variable",
                TotalTerm = 24,
                FixedTerm = 6,
                AprRate = 0.12,
                StartDate = new DateTime(2019, 02, 10),
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 5)
            });

            // Assert
            Assert.IsType<Loan>(result);
            Assert.Equal(10, result.LoanId);
            Assert.Equal("My personal Loan", result.LoanName);
            Assert.Equal("Personal loan for travelling", result.Description);
            Assert.Equal("AIB", result.Institution);
            Assert.True(result.IsActive);
            Assert.Equal("Personal Loan", result.Type);
            Assert.Equal(5000.00m, result.StartPrincipal);
            Assert.Equal("Variable", result.RateType);
            Assert.Equal(24, result.TotalTerm);
            Assert.Equal(6, result.FixedTerm);
            Assert.Equal(0.12, result.AprRate);
            Assert.Equal(new DateTime(2019, 02, 10), result.StartDate);
            Assert.Equal("EUR", result.QuotedCurrency.Code);
            Assert.Equal(5, result.User.UserId);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidLoanId()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.Update(new Loan
            {
                LoanId = 20,
                LoanName = "My personal Loan",
                Description = "Personal loan for travelling",
                Institution = "AIB",
                IsActive = true,
                Type = "Personal Loan",
                StartPrincipal = 5000.00m,
                RateType = "Variable",
                TotalTerm = 24,
                FixedTerm = 6,
                AprRate = 0.12,
                StartDate = new DateTime(2019, 02, 10),
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 5)
            });

            Assert.Null(result);
        }

        [Fact]
        public void Update_ReturnsNull_GivenInvalidUserId()
        {
            var loanRepo = new LoanRepository(_context);

            var result = loanRepo.Update(new Loan
            {
                LoanId = 10,
                LoanName = "My personal Loan",
                Description = "Personal loan for travelling",
                Institution = "AIB",
                IsActive = true,
                Type = "Personal Loan",
                StartPrincipal = 5000.00m,
                RateType = "Variable",
                TotalTerm = 24,
                FixedTerm = 6,
                AprRate = 0.12,
                StartDate = new DateTime(2019, 02, 10),
                QuotedCurrency = _context.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                User = _context.Users.FirstOrDefault(x => x.UserId == 6)
            });

            Assert.Null(result);
        }

    }
}
