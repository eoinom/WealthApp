using backendData;
using backendData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Tests.Infrastructure
{
    public class backendTestDbInitialiser
    {
        public static void EnsureSeedData(backendDbContext db)
        {
            EnsureCountriesSeedData(db);
            EnsureCurrenciesSeedData(db);
            EnsureUsersSeedData(db);
            EnsureUserAccountsSeedData(db);
            EnsureUserLoansSeedData(db);
            EnsureAccountValuesSeedData(db);
            EnsureLoanValuesSeedData(db);
        }

        private static void EnsureCountriesSeedData(backendDbContext db)
        {
            if (!db.Countries.Any())
            {
                var countries = new List<Country>
                {                    
                    new Country { Iso2Code = "AU", Iso3Code = "AUS", Name = "Australia", Continent = "OC" },                   
                    new Country { Iso2Code = "CA", Iso3Code = "CAN", Name = "Canada", Continent = "NA" },                    
                    new Country { Iso2Code = "IE", Iso3Code = "IRL", Name = "Ireland", Continent = "EU" },                    
                    new Country { Iso2Code = "NZ", Iso3Code = "NZL", Name = "New Zealand", Continent = "OC" },                   
                    new Country { Iso2Code = "GB", Iso3Code = "GBR", Name = "United Kingdom", Continent = "EU" },
                    new Country { Iso2Code = "US", Iso3Code = "USA", Name = "United States of America", Continent = "NA" }
                };

                db.Countries.AddRange(countries);
                db.SaveChanges();
            }
        }

        private static void EnsureCurrenciesSeedData(backendDbContext db)
        {
            if (!db.Currencies.Any())
            {
                var currencies = new List<Currency>
                {                   
                    new Currency { Code = "AUD", NameShort = "Dollar", NameLong = "Australian Dollar" },                   
                    new Currency { Code = "CAD", NameShort = "Dollar", NameLong = "Canadian Dollar" },
                    new Currency { Code = "NZD", NameShort = "Dollar", NameLong = "New Zealand Dollar" },
                    new Currency { Code = "GBP", NameShort = "Pound", NameLong = "Pound Sterling" },
                    new Currency { Code = "USD", NameShort = "Dollar", NameLong = "US Dollar" },
                    new Currency { Code = "EUR", NameShort = "Euro", NameLong = "Euro" },
                };

                db.Currencies.AddRange(currencies);
                db.SaveChanges();
            }
        }


        private static void EnsureUsersSeedData(backendDbContext db)
        {
            if (!db.Users.Any())
            {
                var users = new List<User>
                {
                    new User {
                        UserId = 1,
                        Email = "smithie@gmail.com",
                        Password = "jackspassw0rd",
                        FirstName = "Jack",
                        LastName = "Smith",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "NZ"),
                        NewsletterSub = false,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "NZD")
                    },
                    new User {
                        UserId = 2,
                        Email = "joe.bloggs@gmail.com",
                        Password = "pass1234",
                        FirstName = "Joe",
                        LastName = "Bloggs",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "IE"),
                        NewsletterSub = true,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR")
                    },
                    new User {
                        UserId = 3,
                        Email = "jerrym@hotmail.com",
                        Password = "showmethemoney",
                        FirstName = "Jerry",
                        LastName = "Maguire",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "US"),
                        NewsletterSub = false,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "USD")
                    },
                    new User {
                        UserId = 4,
                        Email = "jonesie84@gmail.com",
                        Password = "mypass123",
                        FirstName = "Frank",
                        LastName = "Jones",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "GB"),
                        NewsletterSub = false,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "GBP")
                    },
                    new User {
                        UserId = 5,
                        Email = "ellaon@outlook.com",
                        Password = "pa$$w0Rd",
                        FirstName = "Ella",
                        LastName = "O'Neil",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "AU"),
                        NewsletterSub = false,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "AUD")
                    },
                    new User {
                        UserId = 6,
                        Email = "jwren@aol.com",
                        Password = "beaTles4eva",
                        FirstName = "Jenny",
                        LastName = "Wren",
                        Country = db.Countries.FirstOrDefault(x => x.Iso2Code == "CA"),
                        NewsletterSub = false,
                        DisplayCurrency = db.Currencies.FirstOrDefault(x => x.Code == "CAD")
                    }
                };
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }


        private static void EnsureUserAccountsSeedData(backendDbContext db)
        {
            if (!db.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account {
                        AccountId = 1,
                        AccountName = "Current A/C",
                        Description = "Main joint account",
                        Institution = "Bank of NZ",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "NZD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Account {
                        AccountId = 2,
                        AccountName = "Holiday Savings A/C",
                        Description = "Savings account for family holiday to Australia",
                        Institution = "Credit Union",
                        IsActive = true,
                        Type = "Savings",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "NZD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Account {
                        AccountId = 3,
                        AccountName = "J1 Current A/C",
                        Description = "Old bank account from J1 trip to USA",
                        Institution = "Bank of America",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "USD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Account {
                        AccountId = 4,
                        AccountName = "Investment A/C",
                        Description = "Family investment account",
                        Institution = "JP Morgan",
                        IsActive = true,
                        Type = "Investment",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "GBP"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },

                    new Account {
                        AccountId = 5,
                        AccountName = "Current A/C",
                        Description = "My personal current account",
                        Institution = "Permanent TSB",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },
                    new Account {
                        AccountId = 6,
                        AccountName = "Savings A/C",
                        Description = "Savings account for new car",
                        Institution = "Permanent TSB",
                        IsActive = true,
                        Type = "Savings",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },
                    new Account {
                        AccountId = 7,
                        AccountName = "Australian Current A/C",
                        Description = "Old current account from living in Australia",
                        Institution = "Commonwealth Bank of Australia",
                        IsActive = false,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "AUD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 3)
                    },

                    new Account {
                        AccountId = 8,
                        AccountName = "Current A/C",
                        Description = "General current account",
                        Institution = "Bank of America",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "USD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 3)
                    },
                    new Account {
                        AccountId = 9,
                        AccountName = "Savings A/C",
                        Description = "Rainy day savings account",
                        Institution = "Post Office",
                        IsActive = true,
                        Type = "Savings",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 3)
                    },
                    new Account {
                        AccountId = 10,
                        AccountName = "Savings A/C",
                        Description = "Savings account for leftovers at end of month",
                        Institution = "HSBC",
                        IsActive = true,
                        Type = "Savings",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "GBP"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 4)
                    },
                    new Account {
                        AccountId = 11,
                        AccountName = "My Personal Current A/C",
                        Description = "My personal current account for paying credit card",
                        Institution = "Westpac",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "AUD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 5)
                    },
                    new Account {
                        AccountId = 12,
                        AccountName = "Wife's Personal Current A/C",
                        Description = "Her old personal current account",
                        Institution = "Bank of Montreal",
                        IsActive = true,
                        Type = "Current",
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "CAD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 6)
                    }
                };

                db.Accounts.AddRange(accounts);
                db.SaveChanges();
            }
        }

        private static void EnsureUserLoansSeedData(backendDbContext db)
        {
            if (!db.Loans.Any())
            {
                var loanAccounts = new List<Loan>
                {
                    new Loan {
                        LoanId = 1,
                        LoanName = "Mortgage",
                        Description = "Fist time buyers mortgage for our home",
                        Institution = "Bank of Ireland",
                        IsActive = true,
                        Type = "Mortgage",
                        StartPrincipal = 332500.00m,
                        RateType = "Fixed",
                        TotalTerm = 360,
                        FixedTerm = 60,
                        AprRate = 0.035,
                        StartDate = new DateTime(2018,08,20),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Loan {
                        LoanId = 2,
                        LoanName = "Car Loan",
                        Description = "Loan for new family car - 2014 Skoda Octavia",
                        Institution = "Bank of Ireland",
                        IsActive = true,
                        Type = "Car Loan",
                        StartPrincipal = 10000.00m,
                        RateType = "Fixed",
                        TotalTerm = 60,
                        FixedTerm = 60,
                        AprRate = 0.109,
                        StartDate = new DateTime(2018,09,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Loan {
                        LoanId = 3,
                        LoanName = "Home Improvements",
                        Description = "Loan for home extension",
                        Institution = "Credit Union",
                        IsActive = true,
                        Type = "Home Improvement Loan",
                        StartPrincipal = 40000.00m,
                        RateType = "Variable",
                        TotalTerm = 60,
                        FixedTerm = 0,
                        AprRate = 0.098,
                        StartDate = new DateTime(2018,09,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 1)
                    },
                    new Loan {
                        LoanId = 4,
                        LoanName = "Travel Personal Loan",
                        Description = "Personal loan for travels",
                        Institution = "Commonwealth Bank of Australia",
                        IsActive = false,
                        Type = "Personal Loan",
                        StartPrincipal = 8000.00m,
                        RateType = "Fixed",
                        TotalTerm = 24,
                        FixedTerm = 24,
                        AprRate = 0.12,
                        StartDate = new DateTime(2016,02,10),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "AUD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },

                    new Loan {
                        LoanId = 5,
                        LoanName = "Home Mortgage",
                        Description = "Our home mortgage",
                        Institution = "PTSB",
                        IsActive = true,
                        Type = "Mortgage",
                        StartPrincipal = 400000.00m,
                        RateType = "Fixed",
                        TotalTerm = 360,
                        FixedTerm = 60,
                        AprRate = 0.04,
                        StartDate = new DateTime(2016,03,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },
                    new Loan {
                        LoanId = 6,
                        LoanName = "Investment Mortgage",
                        Description = "Mortgage for our investment property",
                        Institution = "KBC Ireland",
                        IsActive = true,
                        Type = "Mortgage",
                        StartPrincipal = 280000.00m,
                        RateType = "variable",
                        TotalTerm = 240,
                        FixedTerm = 0,
                        AprRate = 0.045,
                        StartDate = new DateTime(2018,01,18),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 2)
                    },
                    new Loan {
                        LoanId = 7,
                        LoanName = "Car Loan",
                        Description = "Loan for new 2018 Ford Focus",
                        Institution = "Credit Union",
                        IsActive = true,
                        Type = "Car Loan",
                        StartPrincipal = 18000.00m,
                        RateType = "Fixed",
                        TotalTerm = 60,
                        FixedTerm = 60,
                        AprRate = 0.099,
                        StartDate = new DateTime(2018,09,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 3)
                    },

                    new Loan {
                        LoanId = 8,
                        LoanName = "Student Loan",
                        Description = "Loan for University Education",
                        Institution = "Credit Union",
                        IsActive = true,
                        Type = "Student Loan",
                        StartPrincipal = 12000.00m,
                        RateType = "Variable",
                        TotalTerm = 60,
                        FixedTerm = 0,
                        AprRate = 0.119,
                        StartDate = new DateTime(2018,09,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 3)
                    },

                    new Loan {
                        LoanId = 9,
                        LoanName = "Travel Personal Loan",
                        Description = "Personal loan for travelling",
                        Institution = "Bank of America",
                        IsActive = false,
                        Type = "Personal Loan",
                        StartPrincipal = 4000.00m,
                        RateType = "Fixed",
                        TotalTerm = 12,
                        FixedTerm = 12,
                        AprRate = 0.125,
                        StartDate = new DateTime(2016,06,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "USD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 4)
                    },

                    new Loan {
                        LoanId = 10,
                        LoanName = "Car Loan",
                        Description = "Loan for new 2016 Tesla Model S",
                        Institution = "AIB",
                        IsActive = true,
                        Type = "Car Loan",
                        StartPrincipal = 40000.00m,
                        RateType = "Variable",
                        TotalTerm = 120,
                        FixedTerm = 0,
                        AprRate = 0.109,
                        StartDate = new DateTime(2019,05,28),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "EUR"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 5)
                    },

                    new Loan {
                        LoanId = 11,
                        LoanName = "Personal Loan",
                        Description = "Personal loan for new drum kit",
                        Institution = "Bank of America",
                        IsActive = false,
                        Type = "Personal Loan",
                        StartPrincipal = 4000.00m,
                        RateType = "Fixed",
                        TotalTerm = 12,
                        FixedTerm = 12,
                        AprRate = 0.125,
                        StartDate = new DateTime(2016,06,01),
                        QuotedCurrency = db.Currencies.FirstOrDefault(x => x.Code == "USD"),
                        User = db.Users.FirstOrDefault(x => x.UserId == 6)
                    },
                };

                db.Loans.AddRange(loanAccounts);
                db.SaveChanges();
            }
        }

        private static void EnsureAccountValuesSeedData(backendDbContext db)
        {
            if (!db.AccountValues.Any())
            {
                var accountValues = new List<AccountValue>
                {
                    new AccountValue { AccountValueId = 1, Date = new DateTime(2018, 2, 7), Value = 3616.6m, RateToUserCurrency = 1.5445, ValueUserCurrency = 5585.84m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },
                    new AccountValue { AccountValueId = 2, Date = new DateTime(2018, 3, 12), Value = 3218.77m, RateToUserCurrency = 1.5782, ValueUserCurrency = 5079.86m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },
                    new AccountValue { AccountValueId = 3, Date = new DateTime(2018, 4, 14), Value = 3701.59m, RateToUserCurrency = 1.5482, ValueUserCurrency = 5730.8m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },
                    new AccountValue { AccountValueId = 4, Date = new DateTime(2018, 5, 2), Value = 3701.59m, RateToUserCurrency = 1.5401, ValueUserCurrency = 5700.82m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },
                    new AccountValue { AccountValueId = 5, Date = new DateTime(2018, 6, 6), Value = 3553.53m, RateToUserCurrency = 1.515, ValueUserCurrency = 5383.6m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },
                    new AccountValue { AccountValueId = 6, Date = new DateTime(2018, 7, 5), Value = 3731.21m, RateToUserCurrency = 1.5374, ValueUserCurrency = 5736.36m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 1) },

                    new AccountValue { AccountValueId = 7, Date = new DateTime(2018, 2, 7), Value = 4400.36m, RateToUserCurrency = 1, ValueUserCurrency = 4400.36m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 2) },
                    new AccountValue { AccountValueId = 8, Date = new DateTime(2018, 3, 12), Value = 4532.37m, RateToUserCurrency = 1, ValueUserCurrency = 4532.37m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 2) },
                    new AccountValue { AccountValueId = 9, Date = new DateTime(2018, 3, 26), Value = 4215.1m, RateToUserCurrency = 1, ValueUserCurrency = 4215.1m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 2) },
                    new AccountValue { AccountValueId = 10, Date = new DateTime(2018, 4, 23), Value = 4088.65m, RateToUserCurrency = 1, ValueUserCurrency = 4088.65m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 2) },
                    new AccountValue { AccountValueId = 11, Date = new DateTime(2018, 5, 20), Value = 4006.88m, RateToUserCurrency = 1, ValueUserCurrency = 4006.88m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 2) },

                    new AccountValue { AccountValueId = 12, Date = new DateTime(2018, 2, 1), Value = 2881.84m, RateToUserCurrency = 1, ValueUserCurrency = 2881.84m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 3) },
                    new AccountValue { AccountValueId = 13, Date = new DateTime(2018, 3, 2), Value = 3256.48m, RateToUserCurrency = 1, ValueUserCurrency = 3256.48m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 3) },
                    new AccountValue { AccountValueId = 14, Date = new DateTime(2018, 3, 26), Value = 3386.74m, RateToUserCurrency = 1, ValueUserCurrency = 3386.74m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 3) },
                    new AccountValue { AccountValueId = 15, Date = new DateTime(2018, 4, 24), Value = 3149.67m, RateToUserCurrency = 1, ValueUserCurrency = 3149.67m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 3) },
                    new AccountValue { AccountValueId = 16, Date = new DateTime(2018, 6, 4), Value = 3622.12m, RateToUserCurrency = 1, ValueUserCurrency = 3622.12m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 3) },

                    new AccountValue { AccountValueId = 17, Date = new DateTime(2018, 1, 21), Value = 2648.7m, RateToUserCurrency = 0.8159934721, ValueUserCurrency = 2161.32m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 4) },
                    new AccountValue { AccountValueId = 18, Date = new DateTime(2018, 2, 21), Value = 2357.34m, RateToUserCurrency = 0.8122157245, ValueUserCurrency = 1914.67m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 4) },
                    new AccountValue { AccountValueId = 19, Date = new DateTime(2018, 3, 30), Value = 2357.34m, RateToUserCurrency = 0.8116224332, ValueUserCurrency = 1913.27m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 4) },
                    new AccountValue { AccountValueId = 20, Date = new DateTime(2018, 4, 28), Value = 2781.66m, RateToUserCurrency = 0.8285004143, ValueUserCurrency = 2304.61m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 4) },
                    new AccountValue { AccountValueId = 21, Date = new DateTime(2018, 6, 12), Value = 2948.56m, RateToUserCurrency = 0.8483203258, ValueUserCurrency = 2501.32m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 4) },

                    new AccountValue { AccountValueId = 22, Date = new DateTime(2018, 1, 22), Value = 2214.08m, RateToUserCurrency = 1.1352670716, ValueUserCurrency = 2513.57m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 5) },
                    new AccountValue { AccountValueId = 23, Date = new DateTime(2018, 2, 14), Value = 2014.81m, RateToUserCurrency = 1.1230907457, ValueUserCurrency = 2262.81m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 5) },
                    new AccountValue { AccountValueId = 24, Date = new DateTime(2018, 3, 19), Value = 2417.77m, RateToUserCurrency = 1.1416437387, ValueUserCurrency = 2760.23m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 5) },
                    new AccountValue { AccountValueId = 25, Date = new DateTime(2018, 4, 13), Value = 2175.99m, RateToUserCurrency = 1.1574074074, ValueUserCurrency = 2518.51m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 5) },
                    new AccountValue { AccountValueId = 26, Date = new DateTime(2018, 5, 4), Value = 1893.11m, RateToUserCurrency = 1.1333371111, ValueUserCurrency = 2145.53m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 5) },

                    new AccountValue { AccountValueId = 27, Date = new DateTime(2018, 2, 7), Value = 292.02m, RateToUserCurrency = 1, ValueUserCurrency = 292.02m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 6) },
                    new AccountValue { AccountValueId = 28, Date = new DateTime(2018, 3, 22), Value = 344.58m, RateToUserCurrency = 1, ValueUserCurrency = 344.58m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 6) },
                    new AccountValue { AccountValueId = 29, Date = new DateTime(2018, 4, 12), Value = 385.93m, RateToUserCurrency = 1, ValueUserCurrency = 385.93m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 6) },
                    new AccountValue { AccountValueId = 30, Date = new DateTime(2018, 5, 24), Value = 335.76m, RateToUserCurrency = 1, ValueUserCurrency = 335.76m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 6) },
                    new AccountValue { AccountValueId = 31, Date = new DateTime(2018, 7, 7), Value = 285.4m, RateToUserCurrency = 1, ValueUserCurrency = 285.4m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 6) },

                    new AccountValue { AccountValueId = 32, Date = new DateTime(2018, 1, 13), Value = 2384.5m, RateToUserCurrency = 1, ValueUserCurrency = 2384.5m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 7) },
                    new AccountValue { AccountValueId = 33, Date = new DateTime(2018, 2, 9), Value = 2217.59m, RateToUserCurrency = 1, ValueUserCurrency = 2217.59m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 7) },
                    new AccountValue { AccountValueId = 34, Date = new DateTime(2018, 3, 15), Value = 2572.4m, RateToUserCurrency = 1, ValueUserCurrency = 2572.4m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 7) },
                    new AccountValue { AccountValueId = 35, Date = new DateTime(2018, 3, 27), Value = 2443.78m, RateToUserCurrency = 1, ValueUserCurrency = 2443.78m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 7) },
                    new AccountValue { AccountValueId = 36, Date = new DateTime(2018, 4, 13), Value = 2346.03m, RateToUserCurrency = 1, ValueUserCurrency = 2346.03m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 7) },

                    new AccountValue { AccountValueId = 37, Date = new DateTime(2018, 2, 3), Value = 3230.4m, RateToUserCurrency = 0.6397952655, ValueUserCurrency = 2066.79m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 8) },
                    new AccountValue { AccountValueId = 38, Date = new DateTime(2018, 3, 11), Value = 3682.66m, RateToUserCurrency = 0.6340349987, ValueUserCurrency = 2334.94m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 8) },
                    new AccountValue { AccountValueId = 39, Date = new DateTime(2018, 4, 20), Value = 4161.41m, RateToUserCurrency = 0.6256647688, ValueUserCurrency = 2603.65m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 8) },
                    new AccountValue { AccountValueId = 40, Date = new DateTime(2018, 5, 28), Value = 3703.65m, RateToUserCurrency = 0.6489292667, ValueUserCurrency = 2403.41m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 8) },
                    new AccountValue { AccountValueId = 41, Date = new DateTime(2018, 6, 10), Value = 3333.29m, RateToUserCurrency = 0.6453278265, ValueUserCurrency = 2151.06m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 8) },

                    new AccountValue { AccountValueId = 42, Date = new DateTime(2018, 1, 19), Value = 1245.12m, RateToUserCurrency = 0.88365, ValueUserCurrency = 1100.25m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 9) },
                    new AccountValue { AccountValueId = 43, Date = new DateTime(2018, 2, 17), Value = 1058.35m, RateToUserCurrency = 0.88803, ValueUserCurrency = 939.85m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 9) },
                    new AccountValue { AccountValueId = 44, Date = new DateTime(2018, 3, 25), Value = 1259.44m, RateToUserCurrency = 0.87285, ValueUserCurrency = 1099.3m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 9) },
                    new AccountValue { AccountValueId = 45, Date = new DateTime(2018, 4, 17), Value = 1297.22m, RateToUserCurrency = 0.8628, ValueUserCurrency = 1119.24m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 9) },
                    new AccountValue { AccountValueId = 46, Date = new DateTime(2018, 5, 25), Value = 1141.55m, RateToUserCurrency = 0.8754, ValueUserCurrency = 999.31m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 9) },

                    new AccountValue { AccountValueId = 47, Date = new DateTime(2018, 2, 5), Value = 752m, RateToUserCurrency = 0.88568, ValueUserCurrency = 666.03m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 10) },
                    new AccountValue { AccountValueId = 48, Date = new DateTime(2018, 3, 22), Value = 721.92m, RateToUserCurrency = 0.872, ValueUserCurrency = 629.51m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 10) },
                    new AccountValue { AccountValueId = 49, Date = new DateTime(2018, 4, 30), Value = 664.17m, RateToUserCurrency = 0.8796, ValueUserCurrency = 584.2m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 10) },
                    new AccountValue { AccountValueId = 50, Date = new DateTime(2018, 6, 6), Value = 617.68m, RateToUserCurrency = 0.87683, ValueUserCurrency = 541.6m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 10) },
                    new AccountValue { AccountValueId = 51, Date = new DateTime(2018, 7, 20), Value = 710.33m, RateToUserCurrency = 0.89445, ValueUserCurrency = 635.35m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 10) },

                    new AccountValue { AccountValueId = 52, Date = new DateTime(2018, 1, 24), Value = 1818.3m, RateToUserCurrency = 1.5229, ValueUserCurrency = 2769.09m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 11) },
                    new AccountValue { AccountValueId = 53, Date = new DateTime(2018, 2, 11), Value = 1945.58m, RateToUserCurrency = 1.5475, ValueUserCurrency = 3010.79m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 11) },
                    new AccountValue { AccountValueId = 54, Date = new DateTime(2018, 3, 11), Value = 2198.51m, RateToUserCurrency = 1.5848, ValueUserCurrency = 3484.2m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 11) },
                    new AccountValue { AccountValueId = 55, Date = new DateTime(2018, 3, 31), Value = 2044.61m, RateToUserCurrency = 1.5895, ValueUserCurrency = 3249.91m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 11) },
                    new AccountValue { AccountValueId = 56, Date = new DateTime(2018, 5, 8), Value = 2228.62m, RateToUserCurrency = 1.5398, ValueUserCurrency = 3431.63m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 11) },

                    new AccountValue { AccountValueId = 57, Date = new DateTime(2018, 2, 8), Value = 3387.78m, RateToUserCurrency = 1.5402, ValueUserCurrency = 5217.86m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 12) },
                    new AccountValue { AccountValueId = 58, Date = new DateTime(2018, 2, 26), Value = 3557.17m, RateToUserCurrency = 1.5617, ValueUserCurrency = 5555.23m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 12) },
                    new AccountValue { AccountValueId = 59, Date = new DateTime(2018, 4, 2), Value = 3841.74m, RateToUserCurrency = 1.5895, ValueUserCurrency = 6106.45m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 12) },
                    new AccountValue { AccountValueId = 60, Date = new DateTime(2018, 4, 15), Value = 3688.07m, RateToUserCurrency = 1.5482, ValueUserCurrency = 5709.87m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 12) },
                    new AccountValue { AccountValueId = 61, Date = new DateTime(2018, 5, 24), Value = 3393.02m, RateToUserCurrency = 1.5111, ValueUserCurrency = 5127.19m, Account = db.Accounts.FirstOrDefault(x => x.AccountId == 12) },
                };
                db.AccountValues.AddRange(accountValues);
                db.SaveChanges();
            }
        }


        private static void EnsureLoanValuesSeedData(backendDbContext db)
        {
            if (!db.LoanValues.Any())
            {
                var loanValues = new List<LoanValue>
                {
                    new LoanValue { LoanValueId = 1, Date = new DateTime(2018, 8, 20), Value = 300000m, RateToUserCurrency = 1, ValueUserCurrency = 300000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 1) },
                    new LoanValue { LoanValueId = 2, Date = new DateTime(2018, 9, 20), Value = 299533.75m, RateToUserCurrency = 1, ValueUserCurrency = 299533.75m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 1) },
                    new LoanValue { LoanValueId = 3, Date = new DateTime(2018, 10, 20), Value = 299066.33m, RateToUserCurrency = 1, ValueUserCurrency = 299066.33m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 1) },
                    new LoanValue { LoanValueId = 4, Date = new DateTime(2018, 11, 20), Value = 298597.75m, RateToUserCurrency = 1, ValueUserCurrency = 298597.75m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 1) },
                    new LoanValue { LoanValueId = 5, Date = new DateTime(2018, 12, 20), Value = 298127.99m, RateToUserCurrency = 1, ValueUserCurrency = 298127.99m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 1) },

                    new LoanValue { LoanValueId = 6, Date = new DateTime(2018, 11, 18), Value = 10000m, RateToUserCurrency = 1, ValueUserCurrency = 10000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 2) },
                    new LoanValue { LoanValueId = 7, Date = new DateTime(2018, 11, 25), Value = 9971.03m, RateToUserCurrency = 1, ValueUserCurrency = 9971.03m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 2) },
                    new LoanValue { LoanValueId = 8, Date = new DateTime(2018, 12, 2), Value = 9942m, RateToUserCurrency = 1, ValueUserCurrency = 9942m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 2) },
                    new LoanValue { LoanValueId = 9, Date = new DateTime(2018, 12, 9), Value = 9912.91m, RateToUserCurrency = 1, ValueUserCurrency = 9912.91m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 2) },
                    new LoanValue { LoanValueId = 10, Date = new DateTime(2018, 12, 16), Value = 9883.76m, RateToUserCurrency = 1, ValueUserCurrency = 9883.76m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 2) },

                    new LoanValue { LoanValueId = 11, Date = new DateTime(2018, 5, 12), Value = 40000m, RateToUserCurrency = 1, ValueUserCurrency = 40000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },
                    new LoanValue { LoanValueId = 12, Date = new DateTime(2018, 6, 12), Value = 39480.72m, RateToUserCurrency = 1, ValueUserCurrency = 39480.72m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },
                    new LoanValue { LoanValueId = 13, Date = new DateTime(2018, 7, 12), Value = 38957.2m, RateToUserCurrency = 1, ValueUserCurrency = 38957.2m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },
                    new LoanValue { LoanValueId = 14, Date = new DateTime(2018, 8, 12), Value = 38429.4m, RateToUserCurrency = 1, ValueUserCurrency = 38429.4m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },
                    new LoanValue { LoanValueId = 15, Date = new DateTime(2018, 9, 12), Value = 37897.29m, RateToUserCurrency = 1, ValueUserCurrency = 37897.29m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },
                    new LoanValue { LoanValueId = 16, Date = new DateTime(2018, 10, 12), Value = 37360.83m, RateToUserCurrency = 1, ValueUserCurrency = 37360.83m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 3) },

                    new LoanValue { LoanValueId = 17, Date = new DateTime(2019, 2, 10), Value = 8000m, RateToUserCurrency = 0.6247657129, ValueUserCurrency = 4998.13m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 18, Date = new DateTime(2019, 2, 17), Value = 7931.85m, RateToUserCurrency = 0.6314725941, ValueUserCurrency = 5008.75m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 19, Date = new DateTime(2019, 2, 24), Value = 7863.54m, RateToUserCurrency = 0.6280223576, ValueUserCurrency = 4938.48m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 20, Date = new DateTime(2019, 3, 3), Value = 7795.08m, RateToUserCurrency = 0.625, ValueUserCurrency = 4871.93m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 21, Date = new DateTime(2019, 3, 10), Value = 7726.46m, RateToUserCurrency = 0.6264094212, ValueUserCurrency = 4839.93m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 22, Date = new DateTime(2019, 3, 17), Value = 7657.68m, RateToUserCurrency = 0.6254691018, ValueUserCurrency = 4789.64m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },
                    new LoanValue { LoanValueId = 23, Date = new DateTime(2019, 3, 24), Value = 7588.74m, RateToUserCurrency = 0.6280223576, ValueUserCurrency = 4765.9m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 4) },

                    new LoanValue { LoanValueId = 24, Date = new DateTime(2018, 3, 1), Value = 400000m, RateToUserCurrency = 1, ValueUserCurrency = 400000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },
                    new LoanValue { LoanValueId = 25, Date = new DateTime(2018, 4, 1), Value = 399423.67m, RateToUserCurrency = 1, ValueUserCurrency = 399423.67m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },
                    new LoanValue { LoanValueId = 26, Date = new DateTime(2018, 5, 1), Value = 398845.42m, RateToUserCurrency = 1, ValueUserCurrency = 398845.42m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },
                    new LoanValue { LoanValueId = 27, Date = new DateTime(2018, 6, 1), Value = 398265.24m, RateToUserCurrency = 1, ValueUserCurrency = 398265.24m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },
                    new LoanValue { LoanValueId = 28, Date = new DateTime(2018, 7, 1), Value = 397683.13m, RateToUserCurrency = 1, ValueUserCurrency = 397683.13m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },
                    new LoanValue { LoanValueId = 29, Date = new DateTime(2018, 8, 1), Value = 397099.08m, RateToUserCurrency = 1, ValueUserCurrency = 397099.08m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 5) },

                    new LoanValue { LoanValueId = 30, Date = new DateTime(2018, 1, 18), Value = 280000m, RateToUserCurrency = 1, ValueUserCurrency = 280000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },
                    new LoanValue { LoanValueId = 31, Date = new DateTime(2018, 4, 18), Value = 277823.5m, RateToUserCurrency = 1, ValueUserCurrency = 277823.5m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },
                    new LoanValue { LoanValueId = 32, Date = new DateTime(2018, 7, 18), Value = 275622.51m, RateToUserCurrency = 1, ValueUserCurrency = 275622.51m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },
                    new LoanValue { LoanValueId = 33, Date = new DateTime(2018, 10, 18), Value = 273396.76m, RateToUserCurrency = 1, ValueUserCurrency = 273396.76m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },
                    new LoanValue { LoanValueId = 34, Date = new DateTime(2019, 1, 18), Value = 271145.97m, RateToUserCurrency = 1, ValueUserCurrency = 271145.97m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },
                    new LoanValue { LoanValueId = 35, Date = new DateTime(2019, 4, 18), Value = 268869.86m, RateToUserCurrency = 1, ValueUserCurrency = 268869.86m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 6) },

                    new LoanValue { LoanValueId = 36, Date = new DateTime(2018, 9, 1), Value = 18000m, RateToUserCurrency = 1, ValueUserCurrency = 18000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },
                    new LoanValue { LoanValueId = 37, Date = new DateTime(2018, 10, 1), Value = 17766.94m, RateToUserCurrency = 1, ValueUserCurrency = 17766.94m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },
                    new LoanValue { LoanValueId = 38, Date = new DateTime(2018, 11, 1), Value = 17531.96m, RateToUserCurrency = 1, ValueUserCurrency = 17531.96m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },
                    new LoanValue { LoanValueId = 39, Date = new DateTime(2018, 12, 1), Value = 17295.04m, RateToUserCurrency = 1, ValueUserCurrency = 17295.04m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },
                    new LoanValue { LoanValueId = 40, Date = new DateTime(2019, 1, 1), Value = 17056.16m, RateToUserCurrency = 1, ValueUserCurrency = 17056.16m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },
                    new LoanValue { LoanValueId = 41, Date = new DateTime(2019, 2, 1), Value = 16815.31m, RateToUserCurrency = 1, ValueUserCurrency = 16815.31m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 7) },

                    new LoanValue { LoanValueId = 42, Date = new DateTime(2018, 9, 1), Value = 12000m, RateToUserCurrency = 0.8974, ValueUserCurrency = 10768.8m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 8) },
                    new LoanValue { LoanValueId = 43, Date = new DateTime(2018, 9, 15), Value = 11932.24m, RateToUserCurrency = 0.89228, ValueUserCurrency = 10646.9m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 8) },
                    new LoanValue { LoanValueId = 44, Date = new DateTime(2018, 9, 29), Value = 11864.17m, RateToUserCurrency = 0.8873, ValueUserCurrency = 10527.08m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 8) },
                    new LoanValue { LoanValueId = 45, Date = new DateTime(2018, 10, 13), Value = 11795.79m, RateToUserCurrency = 0.8764, ValueUserCurrency = 10337.83m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 8) },
                    new LoanValue { LoanValueId = 46, Date = new DateTime(2018, 10, 27), Value = 11727.1m, RateToUserCurrency = 0.8868, ValueUserCurrency = 10399.59m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 8) },

                    new LoanValue { LoanValueId = 47, Date = new DateTime(2018, 6, 1), Value = 4000m, RateToUserCurrency = 1.297626189, ValueUserCurrency = 5190.5m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },
                    new LoanValue { LoanValueId = 48, Date = new DateTime(2018, 7, 1), Value = 3685.34m, RateToUserCurrency = 1.3245839767, ValueUserCurrency = 4881.54m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },
                    new LoanValue { LoanValueId = 49, Date = new DateTime(2018, 8, 1), Value = 3367.4m, RateToUserCurrency = 1.3022400821, ValueUserCurrency = 4385.16m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },
                    new LoanValue { LoanValueId = 50, Date = new DateTime(2018, 9, 1), Value = 3046.15m, RateToUserCurrency = 1.3039224101, ValueUserCurrency = 3971.94m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },
                    new LoanValue { LoanValueId = 51, Date = new DateTime(2018, 10, 1), Value = 2721.55m, RateToUserCurrency = 1.2809753576, ValueUserCurrency = 3486.24m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },
                    new LoanValue { LoanValueId = 52, Date = new DateTime(2018, 11, 1), Value = 2393.57m, RateToUserCurrency = 1.3081716844, ValueUserCurrency = 3131.2m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 9) },

                    new LoanValue { LoanValueId = 53, Date = new DateTime(2019, 1, 28), Value = 40000m, RateToUserCurrency = 1, ValueUserCurrency = 40000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },
                    new LoanValue { LoanValueId = 54, Date = new DateTime(2019, 2, 28), Value = 39814.59m, RateToUserCurrency = 1, ValueUserCurrency = 39814.59m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },
                    new LoanValue { LoanValueId = 55, Date = new DateTime(2019, 3, 28), Value = 39627.5m, RateToUserCurrency = 1, ValueUserCurrency = 39627.5m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },
                    new LoanValue { LoanValueId = 56, Date = new DateTime(2019, 4, 28), Value = 39438.71m, RateToUserCurrency = 1, ValueUserCurrency = 39438.71m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },
                    new LoanValue { LoanValueId = 57, Date = new DateTime(2019, 5, 28), Value = 39248.2m, RateToUserCurrency = 1, ValueUserCurrency = 39248.2m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },
                    new LoanValue { LoanValueId = 58, Date = new DateTime(2019, 6, 28), Value = 39055.96m, RateToUserCurrency = 1, ValueUserCurrency = 39055.96m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 10) },

                    new LoanValue { LoanValueId = 59, Date = new DateTime(2019, 3, 1), Value = 4000m, RateToUserCurrency = 1, ValueUserCurrency = 4000m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                    new LoanValue { LoanValueId = 60, Date = new DateTime(2019, 3, 8), Value = 3927.7m, RateToUserCurrency = 1, ValueUserCurrency = 3927.7m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                    new LoanValue { LoanValueId = 61, Date = new DateTime(2019, 3, 15), Value = 3855.22m, RateToUserCurrency = 1, ValueUserCurrency = 3855.22m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                    new LoanValue { LoanValueId = 62, Date = new DateTime(2019, 3, 22), Value = 3782.57m, RateToUserCurrency = 1, ValueUserCurrency = 3782.57m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                    new LoanValue { LoanValueId = 63, Date = new DateTime(2019, 3, 29), Value = 3709.74m, RateToUserCurrency = 1, ValueUserCurrency = 3709.74m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                    new LoanValue { LoanValueId = 64, Date = new DateTime(2019, 4, 5), Value = 3636.74m, RateToUserCurrency = 1, ValueUserCurrency = 3636.74m, Loan = db.Loans.FirstOrDefault(x => x.LoanId == 11) },
                };
                db.LoanValues.AddRange(loanValues);
                db.SaveChanges();
            }
        }
    }
}