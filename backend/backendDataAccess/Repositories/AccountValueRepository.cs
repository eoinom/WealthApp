﻿using backendData;
using backendData.Models;
using backendDataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backendDataAccess.Repositories
{
    public class AccountValueRepository : IAccountValueRepository
    {
        private readonly backendDbContext _dbContext;

        public AccountValueRepository(backendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccountValue Add(AccountValue value)
        {
            if (value.AccountValueId > 0 && _dbContext.AccountValues.Any(x => x.AccountValueId == value.AccountValueId))
            {
                return null;
            }

            if (value.Account != null)
            {
                Account account = _dbContext.Accounts
                                            //.Include(x => x.AccountValues)
                                            .Include(x => x.QuotedCurrency)
                                            .SingleOrDefault(x => x.AccountId == value.Account.AccountId);
                value.Account = account;
            }

            _dbContext.AccountValues.Add(value);
            _dbContext.SaveChanges();
            _dbContext.Entry<AccountValue>(value).State = EntityState.Detached;
            _dbContext.Entry<Account>(value.Account).State = EntityState.Detached;
            _dbContext.Entry<Currency>(value.Account.QuotedCurrency).State = EntityState.Detached;
            _dbContext.SaveChanges();
            return value;
        }

        public void DeleteById(int valueId)
        {
            var value = _dbContext.AccountValues.SingleOrDefault(x => x.AccountValueId == valueId);
            _dbContext.Remove(value);
            _dbContext.SaveChanges();
        }

        public void DeleteByIds(int[] valueIds)
        {
            foreach (var valueId in valueIds)
            {
                var value = _dbContext.AccountValues.SingleOrDefault(x => x.AccountValueId == valueId);
                _dbContext.Remove(value);
            }           
            _dbContext.SaveChanges();
        }

        public IEnumerable<AccountValue> GetAllForAccount(int accountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.Account.AccountId == accountId)
                .Include(x => x.Account)
                .Include(x => x.Account.QuotedCurrency)
                .Include(x => x.Account.User);
        }

        public AccountValue GetLatestForAccount(int accountId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Where(x => x.Account.AccountId == accountId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.Account)
                .Include(x => x.Account.QuotedCurrency)
                .Include(x => x.Account.User)
                .FirstOrDefault();
        }

        public AccountValue GetById(int valueId)
        {
            return _dbContext.AccountValues.AsNoTracking()
                .Include(x => x.Account)
                .Include(x => x.Account.QuotedCurrency)
                .Include(x => x.Account.User)
                .SingleOrDefault(x => x.AccountValueId == valueId);
        }

        public AccountValue Update(AccountValue valueUpdates)
        {
            if (valueUpdates.Account != null)
            {
                valueUpdates.Account = _dbContext.Accounts.AsNoTracking()
                                        .Include(x => x.QuotedCurrency)
                                        .SingleOrDefault(x => x.AccountId == valueUpdates.Account.AccountId);
            }

            var accountValue = _dbContext.AccountValues
                .Include(x => x.Account)
                    .ThenInclude(u => u.QuotedCurrency)
                .Include(x => x.Account)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.DisplayCurrency)
                .SingleOrDefault(x => x.AccountValueId == valueUpdates.AccountValueId && x.Account.AccountId == valueUpdates.Account.AccountId);

            if (accountValue != null)
            {
                accountValue.Date = valueUpdates.Date;
                accountValue.Value = valueUpdates.Value;
                accountValue.RateToUserCurrency = valueUpdates.RateToUserCurrency;
                accountValue.ValueUserCurrency = valueUpdates.ValueUserCurrency;

                _dbContext.SaveChanges();

                //Note: need to detach to avoid tracking error when trying to update the same entry again with the same context
                //See: https://entityframeworkcore.com/knowledge-base/50987635/the-instance-of-entity-type--item--cannot-be-tracked-because-another-instance-with-the-same-key-value-for---id---is-already-being-tracked
                _dbContext.Entry<AccountValue>(accountValue).State = EntityState.Detached;
                _dbContext.Entry<Account>(accountValue.Account).State = EntityState.Detached;
                _dbContext.Entry<Currency>(accountValue.Account.QuotedCurrency).State = EntityState.Detached;
                _dbContext.Entry<User>(accountValue.Account.User).State = EntityState.Detached;
                _dbContext.Entry<Currency>(accountValue.Account.User.DisplayCurrency).State = EntityState.Detached;
                _dbContext.SaveChanges();
            }
            return accountValue;
        }
    }
}
