using BankingSystem.Application.Controllers;
using BankingSystem.Business.IServices;
using BankingSystem.Domain.Entity;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankingSystem.XUnitTest
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> mockRepo = new();
        private readonly Mock<ILogger<AccountController>> mockLogger = new();

        [Fact]
        public async Task CreateAccountAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange
            
            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            var result =await controller.CreateAccountAsync(account);

            //// Assert
            Assert.IsType<OkObjectResult>(result);
           
        }

        [Fact]
        public async Task CreateAccountAsync_ReturnsBadRequestResult_WhenDataInvalid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            account.Amount = 90;
            var result = await controller.CreateAccountAsync(account);

            //// Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task GetAllAccountAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            var result = await controller.GetAllAccountAsync();

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            var result = await controller.GetAccountByIdAsync(1);

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DeleteAccountByIdAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            var result = await controller.DeleteAccountByIdAsync(1);

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DeleteAccountByIdAsync_ReturnsNotFoundResult_WhenDataInvalid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(2)).ReturnsAsync(account);
            var result = await controller.DeleteAccountByIdAsync(1);

            //// Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task UpdateAccountAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            var result = await controller.DeleteAccountByIdAsync(1);

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task UpdateAccountAsync_ReturnsNotFoundResult_WhenDataInvalid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(2)).ReturnsAsync(account);
            var result = await controller.UpdateAccountAsync(account);

            //// Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task DepositAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            var result = await controller.Deposit(account);

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DepositAsync_ReturnsNotFoundResult_WhenDataInvalid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(2)).ReturnsAsync(account);
            var result = await controller.Deposit(account);

            //// Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task DepositAsync_ReturnsBadRequestObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            account.Amount = -9;
            var result = await controller.Deposit(account);

            //// Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task WithdrawAsync_ReturnsOkObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);            
            var result = await controller.Withdraw(accountTemp);

            //// Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task WithdrawAsync_ReturnsNotFoundResult_WhenDataInvalid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(2)).ReturnsAsync(account);
            var result = await controller.Withdraw(account);

            //// Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task WithdrawAsync_ReturnsBadRequestObjectResult_WhenDataValid()
        {
            // Arrange

            var controller = new AccountController(mockLogger.Object, mockRepo.Object);
            mockRepo.Setup(repo => repo.GetAccountByIdAsync(1)).ReturnsAsync(account);
            account.Amount = -9;
            var result = await controller.Withdraw(account);

            //// Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        AccountEntity account = new AccountEntity()
        {
            AccountNumber = 1,
            Amount = 1200,
            Name = "Test"
        };
        AccountEntity accountTemp = new AccountEntity()
        {
            AccountNumber = 1,
            Amount = 200,
            Name = "Test"
        };
    }
}
