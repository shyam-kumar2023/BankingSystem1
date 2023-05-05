using BankingSystem.Business.IServices;
using BankingSystem.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
               

        [HttpGet("GetAccountById/{id}")]
        public async Task<ActionResult> GetAccountByIdAsync(int id)
        {
            try
            {
                var result = await _accountService.GetAccountByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllAccount")]
        public async Task<ActionResult> GetAllAccountAsync()
        {
            try
            {
                var result = await _accountService.GetAllAccountAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAccountAsync([FromBody] AccountEntity entity)
        {
            try
            {
                if (entity.Amount < 100)
                {
                    return BadRequest("An account cannot have less than $100");
                }
                var result = await _accountService.CreateAccountAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Account")]
        public async Task<ActionResult> UpdateAccountAsync([FromBody] AccountEntity account)
        {
            try
            {
                var data = await _accountService.GetAccountByIdAsync(account.AccountNumber);
                if (data == null)
                {
                    return NotFound();
                }
                if (data.Amount<100)
                {
                    return BadRequest("An account cannot have less than $100");
                }

                var result = await _accountService.UpdateAccountAsync(account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("Account")]
        public async Task<ActionResult> DeleteAccountByIdAsync(int id)
        {
            try
            {
                var data = await _accountService.GetAccountByIdAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                
                var result = await _accountService.DeleteAccountByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Deposit")]
        public async Task<ActionResult> Deposit([FromBody] AccountEntity account)
        {
            try
            {
                var data = await _accountService.GetAccountByIdAsync(account.AccountNumber);
                if (data == null)
                {
                    return NotFound();
                }
                if (account.Amount <= 0)
                {
                    return BadRequest("An account cannot have less than 0");
                }
                data.Amount += account.Amount;
                var result = await _accountService.UpdateAccountAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("Withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] AccountEntity account)
        {
            try
            {
                var data = await _accountService.GetAccountByIdAsync(account.AccountNumber);
                if (data == null)
                {
                    return NotFound();
                }
                if (account.Amount <= 0)
                {
                    return BadRequest("Cannot withdraw less than 0");
                }
                var per = (account.Amount / data.Amount) * 100;
                if (per>90)
                {
                    return BadRequest("Account cannot withdraw more than 90% of their total balance from an account in a single transaction.");
                }
                var amount = (per / 100) * data.Amount;
                if ((data.Amount-amount)<100)
                {
                    return BadRequest("An account cannot have less than $100");
                }

                data.Amount -= account.Amount;
                var result = await _accountService.UpdateAccountAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
