using BankApplication.DTO;
using BankApplication.Models;
using BankApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepo _accountRepo;
        private IEnumerable<IInterestCalculatorRepo> _interestCalculatorRepo;
        public AccountController(BankDbContext bankDbContext, IAccountRepo accountRepo, IEnumerable<IInterestCalculatorRepo> interestCalculateRepo)
        {

            _accountRepo = accountRepo;
            _interestCalculatorRepo = interestCalculateRepo;
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddAccount([FromBody] AccountDto accountDto)
        {
            var response = await _accountRepo.AddAccount(accountDto);
            if (response.Success)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);

        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<AccountShowDto>>>> GetAllAccounts()
        {
            var response = await _accountRepo.GetAllAccounts();
            if (response.Success)
            {
                return Ok(response);
            }

            return Ok(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseDto<AccountDto>>> DeleteAccount(int id)
        {
            var response = await _accountRepo.DeleteAccount(id);
            if (response.Success)
            {
                return Ok(response);
            }

            return Ok(response);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseDto<AccountDto>>> UpdateAccount(int id, [FromBody] AccountDto accountDto)
        {
            var response = await _accountRepo.UpdateAccount(id, accountDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return Ok(response);

        }

        [HttpPut("{accno:int}")]
        public async Task<ActionResult<ResponseDto<AccountDto>>> UpdateAccountStatus(int accno)
        {
            var response = await _accountRepo.AccountStatusChange(accno);
            if (response.Success)
            {
                return Ok(response.Message);
            }

            return BadRequest(response.Message);

        }

      
       

        [HttpGet("{CustomerId:int}")]
        public async Task<ActionResult<ResponseDto<AccountShowDto>>> GetAccountByCustomerId(int CustomerId)
        {
            var response = await _accountRepo.GetAccountByCustomerId(CustomerId);
            if (response.Success)
            {
                return Ok(response);
            }

            return Ok(response);

        }
        [HttpGet("{accountType}")]
        public async Task<ActionResult<ResponseDto<AccountDto>>> GetInterestRate(string accountType)
        {
            var interestRate = _interestCalculatorRepo.FirstOrDefault(x => x.AccountType == accountType);
            if (interestRate != null)
            {
                var interest = await interestRate.GetInterestRate();
                if (interest != -1)
                {
                    return Ok(interest);
                }
                return NotFound();
            }
            return BadRequest();

        }

    }
}
