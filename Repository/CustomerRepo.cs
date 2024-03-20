using AutoMapper;

using BankApplication.DTO;
using BankApplication.Models;
using BankApplication.Repository;
using BankManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankManagement.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly IAccountRepo _accountRepo;
        public CustomerRepo(BankDbContext context, IMapper mapper, ILogger<CustomerController> logger, IAccountRepo accountRepo)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _accountRepo = accountRepo;
        }

        public async Task<ResponseDto<CustomerDto>> AddCustomer(CustomerInputDto customerInputDto)
        {
            try
            {
                
                var tempObj= await _context.Customers.FirstOrDefaultAsync(x=> x.AadharNumber== customerInputDto.AadharNumber);
                if (tempObj==null)
                {
                    var custObj = _mapper.Map<Customer>(customerInputDto);
                    await _context.Customers.AddAsync(custObj);
                    await _context.SaveChangesAsync();

                    try
                    {
                        AccountDto accountDto = new AccountDto();
                        var obj = _mapper.Map<AccountDto>(customerInputDto);
                        obj.CustomerId = custObj.CustomerId;
                        obj.Status = true;
                        await _accountRepo.AddAccount(obj);
                        await _context.SaveChangesAsync();
                        var response = new ResponseDto<CustomerDto>
                        {
                            Success = true,
                            Message = "Customer Added Sucessfully",
                        };
                        return response;
                    }
                    catch(Exception ex)
                    {
                       var delObj= await _context.Customers.FirstOrDefaultAsync(x=>x.AadharNumber == customerInputDto.AadharNumber);
                        if (delObj != null)
                        {
                             _context.Customers.Remove(delObj);
                            await _context.SaveChangesAsync();
                        }
                        var response = new ResponseDto<CustomerDto>
                        {
                            Success = false,
                            Message = "Account DEetails went wrong",
                        };
                        return response;

                    }
    
                }
         
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new ResponseDto<CustomerDto>
            {
                Success = false,
                Message = "Failure"
            };
        }
        public async Task<ResponseDto<List<CustomerDto>>> GetAllCustomers()
        {
            try
            {
                var customerList = await _context.Customers.Select(customer => _mapper.Map<CustomerDto>(customer)).ToListAsync();
                var responseDto = new ResponseDto<List<CustomerDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = customerList
                };
                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<List<CustomerDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching customers.",
                };
                return errorResponse;
            }
        }

        public async Task<ResponseDto<List<CustomerDetailsShowDto>>> GetAllCustomerWithAccountDetails()
        {
            try
            {
                var custList = await (from customer in _context.Customers
                                      join account in _context.Accounts on customer.CustomerId equals account.CustomerId
                                      select new CustomerDetailsShowDto
                                      {
                                          FirstName = customer.FirstName,
                                          LastName = customer.LastName,
                                          PhoneNumber = customer.PhoneNumber,
                                          Email = customer.Email,
                                          DateOfBirth = customer.DateOfBirth,
                                          AadharNumber = customer.AadharNumber,
                                          Address = customer.Address,
                                          AccountNo = account.AccountNo,
                                          AccountType = account.AccountType,
                                          Balance = account.Balance,
                                          AccountOpenDate = account.AccountOpenDate,
                                          Status = account.Status
                                      }).ToListAsync();

                var responseDto = new ResponseDto<List<CustomerDetailsShowDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = custList
                };

                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<List<CustomerDetailsShowDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching customers.",
                };

                return errorResponse;
            }
        }


        public async Task<ResponseDto<CustomerDto>> DeleteCustomer(string aadharNumber)
        {
            try
            {
                var custObj = await _context.Customers.FirstOrDefaultAsync(x => x.AadharNumber == aadharNumber);
                if (custObj != null)
                {
                    _context.Customers.Remove(custObj);
                    await _context.SaveChangesAsync();
                    var responseDto = new ResponseDto<CustomerDto>
                    {
                        Success = true,
                        Message = "Customer Deleted Sucessfully"

                    };
                    return responseDto;
                }
                else
                {
                    var errorResponse = new ResponseDto<CustomerDto>
                    {
                        Success = false,
                        Message = "Customer Not Found"

                    };

                    return errorResponse;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<CustomerDto>
                {
                    Success = false,
                    Message = "An error occurred while fetching customers."
                };

                return errorResponse;
            }

        }


        public async Task<ResponseDto<CustomerDto>> UpdateCustomer(int id, [FromBody] CustomerInputDto customerInputDto)
        {
            try
            {
                var custObj = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
                var accObj = await _context.Accounts.FirstOrDefaultAsync(x=>x.CustomerId == id);
                if (custObj != null)
                {

                    var customerObj=_mapper.Map(customerInputDto,custObj);
                    
                    _context.Customers.Update(customerObj);
                    var accountObj=_mapper.Map(customerInputDto,accObj);  
                    _context.Accounts.Update(accountObj);
                    await _context.SaveChangesAsync();

                    var responseDto = new ResponseDto<CustomerDto>
                    {
                        Success = true,
                        Message = "Customer Updated Successfully",
                        
                    };

                    return responseDto;
                }
                else
                {
                    var errorResponse = new ResponseDto<CustomerDto>
                    {
                        Success = false,
                        Message = "Customer not found."
                    };

                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<CustomerDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the customer."
                };

                return errorResponse;
            }
        }

        public async Task<ResponseDto<CustomerDetailsShowDto>> GetCustomerWithAccountDetailsByAadharNo(string aadharNumber)
        {
            try
            {
                var result = await (from customer in _context.Customers
                              join account in _context.Accounts on customer.CustomerId equals account.CustomerId
                              where customer.AadharNumber == aadharNumber
                              select new CustomerDetailsShowDto
                              {
                                  CustomerId = customer.CustomerId,
                                  FirstName = customer.FirstName,
                                  LastName = customer.LastName,
                                  PhoneNumber = customer.PhoneNumber,
                                  Email = customer.Email,
                                  DateOfBirth = customer.DateOfBirth,
                                  AadharNumber = customer.AadharNumber,
                                  Address = customer.Address,
                                  AccountNo = account.AccountNo,
                                  AccountType = account.AccountType,
                                  Balance = account.Balance,
                                  AccountOpenDate = account.AccountOpenDate,
                                  Status = account.Status
                              }).FirstOrDefaultAsync();

                if (result != null)
                {
                    return new ResponseDto<CustomerDetailsShowDto>
                    {
                        Success = true,
                        Message = "Success",
                        Data = (CustomerDetailsShowDto)result
                    };
                }
                else
                {
                    return new ResponseDto<CustomerDetailsShowDto>
                    {
                        Success = false,
                        Message = "Aadhar Number not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return new ResponseDto<CustomerDetailsShowDto>
                {
                    Success = false,
                    Message = "Failure",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<CustomerDetailsShowDto>> GetCustomerWithAccountDetailsBycustomerId(int CustomerId)
        {
            try
            {
                var result = await (from customer in _context.Customers
                                    join account in _context.Accounts on customer.CustomerId equals account.CustomerId
                                    where customer.CustomerId == CustomerId
                                    select new CustomerDetailsShowDto
                                    {
                                        FirstName = customer.FirstName,
                                        LastName = customer.LastName,
                                        PhoneNumber = customer.PhoneNumber,
                                        Email = customer.Email,
                                        DateOfBirth = customer.DateOfBirth,
                                        AadharNumber = customer.AadharNumber,
                                        Address = customer.Address,
                                        AccountNo = account.AccountNo,
                                        AccountType = account.AccountType,
                                        Balance = account.Balance,
                                        AccountOpenDate = account.AccountOpenDate,
                                        Status = account.Status
                                    }).FirstOrDefaultAsync();

                if (result != null)
                {
                    return new ResponseDto<CustomerDetailsShowDto>
                    {
                        Success = true,
                        Message = "Success",
                        Data = (CustomerDetailsShowDto)result
                    };
                }
                else
                {
                    return new ResponseDto<CustomerDetailsShowDto>
                    {
                        Success = false,
                        Message = "customer  not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return new ResponseDto<CustomerDetailsShowDto>
                {
                    Success = false,
                    Message = "Failure",
                    
                };
            }
        }


    }

}


    
