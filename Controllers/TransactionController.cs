using AutoMapper;
using BankApplication.DTO;
using BankApplication.Models;
using BankApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepo _transactionRepo;
        public TransactionController(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;

        }
        [HttpPost] 
        public async Task<ActionResult<string>> AddTransaction([FromBody] TransactionInputDto transactionDto)
        {
            var response = await _transactionRepo.AddTransaction(transactionDto);

            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);

        }
        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetAlllTransactions()
        {
            var response = await _transactionRepo.GetAllTransactions();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

       

        [HttpGet("{AccountNo:int}")]
        public async Task<ActionResult<TransactionDto>> GetAlllTransactionsByAccountNumber(int AccountNo)
        {
            var response = await _transactionRepo.GetAlllTransactionsByAccountNumber(AccountNo);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

       
    }
}
