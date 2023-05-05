using BankingSystem.Data.IRepository;
using BankingSystem.Data.Repository;
using BankingSystem.Domain;
using BankingSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankingSystem.XUnitTest
{
    public class AccountRepositoryTest
    {       
        private readonly Mock<ILogger<AccountRepository>> mockLogger = new();
                
        [Fact]
        public async Task GetAccountByIdAsync_ReturnsData()
        {
            IAccountRepository sut = GetInMemoryPersonRepository();
            await sut.CreateAccountAsync(account);
            var result = await sut.GetAccountByIdAsync(1);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetAllAccountAsync_ReturnsData()
        {
            IAccountRepository sut = GetInMemoryPersonRepository();
            var result = await sut.GetAllAccountAsync();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreateAccountAsync_ReturnsData()
        {
            IAccountRepository sut = GetInMemoryPersonRepository(); 
            var result =await sut.CreateAccountAsync(account);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UpdateAccountAsync_ReturnsData()
        {
            IAccountRepository sut = GetInMemoryPersonRepository();
            await sut.CreateAccountAsync(account);
            var result = await sut.UpdateAccountAsync(account);
            Assert.NotNull(result);
        }

        private IAccountRepository GetInMemoryPersonRepository()
        {
            DbContextOptions<PGDbContext> options;
            var builder = new DbContextOptionsBuilder<PGDbContext>();
            builder.UseInMemoryDatabase("Test");
            options = builder.Options;
            PGDbContext personDataContext = new(options);
            personDataContext.Database.EnsureDeleted();
            personDataContext.Database.EnsureCreated();
            return new AccountRepository(mockLogger.Object,personDataContext);
        }

        AccountEntity account = new AccountEntity()
        {
            AccountNumber = 1,
            Name = "fred",
            Amount = 100
        };
    }
}
