using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Business.IServices
{
    public interface IAccountService
    {
        Task<AccountEntity> GetAccountByIdAsync(int id);
        Task<AccountEntity> CreateAccountAsync(AccountEntity entity);

        Task<AccountEntity> UpdateAccountAsync(AccountEntity account);
        Task<IEnumerable<AccountEntity>> GetAllAccountAsync();
        Task<bool> DeleteAccountByIdAsync(int id);
    }
}
