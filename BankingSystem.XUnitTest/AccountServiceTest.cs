using BankingSystem.Business.Services;
using BankingSystem.Data.IRepository;
using BankingSystem.Domain.Entity;
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
    public class AccountServiceTest
    {
        private readonly Mock<IAccountRepository> mockRepo = new();
        private readonly Mock<ILogger<AccountService>> mockLogger = new();

        [Fact]
        public async Task CreateAccountAsync_ReturnsData()
        {
            // Arrange

            var service = new AccountService(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.CreateAccountAsync(account)).ReturnsAsync(account);
            var result = await service.CreateAccountAsync(account);

            //// Assert
            Assert.Equal(result,account);
        }

        [Fact]
        public async Task UpdateAccountAsync_ReturnsData()
        {
            // Arrange

            var service = new AccountService(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.UpdateAccountAsync(account)).ReturnsAsync(account);
            var result = await service.UpdateAccountAsync(account);

            //// Assert
            Assert.Equal(result, account);
        }

        [Fact]
        public async Task GetAllAccountAsync_ReturnsData()
        {
            // Arrange

            var service = new AccountService(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAllAccountAsync()).ReturnsAsync(list);
            var result = await service.GetAllAccountAsync();

            //// Assert
            Assert.Equal(result, list);
        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnsData()
        {
            // Arrange

            var service = new AccountService(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            var result = await service.GetAccountByIdAsync(1);

            //// Assert
            Assert.Equal(result, account);
        }
        AccountEntity account = new AccountEntity()
        {
            AccountNumber = 1,
            Amount = 200,
            Name = "Test"
        };
        List<AccountEntity> list = new List<AccountEntity>()
        {
            new AccountEntity()
        {
            AccountNumber = 1,
            Amount = 200,
            Name = "Test"
        }
        };

        AccountEntity accountReturn = new AccountEntity()
        {
            AccountNumber = 1,
            Amount = 200,
            Name = "Test"
        };
    }
}
