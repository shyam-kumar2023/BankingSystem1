using BankingSystem.Business.IServices;
using BankingSystem.Data.IRepository;
using BankingSystem.Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ILogger<AccountService> logger, IAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<AccountEntity> CreateAccountAsync(AccountEntity entity)
        {
            try
            {
                return await _repository.CreateAccountAsync(entity);
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
                return await _repository.GetAllAccountAsync();
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
                return await _repository.GetAccountByIdAsync(id);
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
                return await _repository.UpdateAccountAsync(account);
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
                return await _repository.DeleteAccountByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
