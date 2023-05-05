using BankingSystem.Data.IRepository;
using BankingSystem.Domain;
using BankingSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PGDbContext _dbContext;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ILogger<AccountRepository> logger, PGDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<AccountEntity> CreateAccountAsync(AccountEntity entity)
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }

        public async Task<IEnumerable<AccountEntity>> GetAllAccountAsync()
        {
            try
            {
                return await _dbContext.Accounts.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
           
        }

        public async Task<AccountEntity> GetAccountByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<AccountEntity> UpdateAccountAsync(AccountEntity account)
        {
            try
            {
                var entity = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == account.AccountNumber);
                entity.Amount = account.Amount;
                entity.Name = account.Name;
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public async Task<bool> DeleteAccountByIdAsync(int id)
        {
            try
            {
                var entity =  _dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == id);
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
