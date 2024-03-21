using Azure;
using BankApplication.DTO;
using BankApplication.Models;
using BankApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private ICustomerRepo _customerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {

            _customerRepo = customerRepo;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddCustomer([FromBody] CustomerInputDto customerInputDto)
        {

            var response = await _customerRepo.AddCustomer(customerInputDto);
            if (response.Success)
            {
                return Ok("success");

            }
            return Ok(response.Message);

        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CustomerDetailsShowDto>>>> GetAllCustomers()
        {
            var response = await _customerRepo.GetAllCustomers();
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CustomerDetailsShowDto>>>> GetAllCustomerWithAccountDetails()
        {
            var response = await _customerRepo.GetAllCustomerWithAccountDetails();
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response);


        }

        [HttpDelete("{aadharNumber}")]
        public async Task<ActionResult<string>> DeleteCustomer(string aadharNumber)
        {
            var response = await _customerRepo.DeleteCustomer(aadharNumber);
            if (response.Success)
            {
                return Ok("success");
            }

            return BadRequest(response.Message);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> UpdateCustomer(int id, [FromBody] CustomerInputDto customerInputDto)
        {
            var response = await _customerRepo.UpdateCustomer(id, customerInputDto);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);

        }
        [HttpGet("{AadharNumber}")]
        public async Task<ActionResult<ResponseDto<CustomerDetailsShowDto>>> GetCustomerWithAccountDetailsByAadharNo(String AadharNumber)
        {
            var response = await _customerRepo.GetCustomerWithAccountDetailsByAadharNo(AadharNumber);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        [HttpGet("{CustomerId:int}")]
        public async Task<ActionResult<ResponseDto<CustomerDetailsShowDto>>> GetCustomerWithAccountDetailsBycustomerId(int CustomerId)
        {
            var response = await _customerRepo.GetCustomerWithAccountDetailsBycustomerId(CustomerId);
            if (response.Success)
            {
                return Ok(response);
            }

            return Ok(response);


        }
    }
}
