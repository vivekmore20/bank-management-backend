using AutoMapper;

using BankApplication.DTO;
using BankApplication.Models;
using BankApplication.Repository;
using BankManagement.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;


        public AccountRepo(BankDbContext context, IMapper mapper, ILogger<AccountController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
         
        }

        public async Task<ResponseDto<AccountDto>> AddAccount(AccountDto accountDto)
        {
            try
            {
                var accountObj = _mapper.Map<Account>(accountDto);
                if (accountObj != null)
                {
                    accountObj.AccountOpenDate = DateTime.Now;
                    accountObj.Status = true;
                    await _context.Accounts.AddAsync(accountObj);
                    await _context.SaveChangesAsync();
                    var response = new ResponseDto<AccountDto>
                    {
                        Success = true,
                        Message = "Account Added Successfully",
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return new ResponseDto<AccountDto>
            {
                Success = false,
                Message = "Failure"
            };

        }

        public async Task<ResponseDto<List<AccountShowDto>>> GetAllAccounts()
        {
            try
            {
                var accountList = await _context.Accounts.Select(account => _mapper.Map<AccountShowDto>(account)).ToListAsync();
                var responseDto = new ResponseDto<List<AccountShowDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = accountList
                };
                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<List<AccountShowDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching accounts.",
                };
                return errorResponse;
            }
        }

        public async Task<ResponseDto<AccountDto>> DeleteAccount(int id)
        {
            try
            {
                var accountObj = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == id);
                if (accountObj != null)
                {
                    _context.Accounts.Remove(accountObj);
                    await _context.SaveChangesAsync();
                    var responseDto = new ResponseDto<AccountDto>
                    {
                        Success = true,
                        Message = "Account Deleted Successfully"
                    };
                    return responseDto;
                }
                else
                {
                    var errorResponse = new ResponseDto<AccountDto>
                    {
                        Success = false,
                        Message = "Account Not Found"
                    };
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<AccountDto>
                {
                    Success = false,
                    Message = "An error occurred while deleting the account."
                };
                return errorResponse;
            }
        }

        public async Task<ResponseDto<AccountDto>> UpdateAccount(int id, AccountDto accountDto)
        {
            try
            {
                var accountObj = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == id);
                if (accountObj != null)
                {
                    _mapper.Map(accountDto, accountObj);
                    _context.Accounts.Update(accountObj);
                    await _context.SaveChangesAsync();

                    var responseDto = new ResponseDto<AccountDto>
                    {
                        Success = true,
                        Message = "Account Updated Successfully",
                        Data = accountDto
                    };

                    return responseDto;
                }
                else
                {
                    var errorResponse = new ResponseDto<AccountDto>
                    {
                        Success = false,
                        Message = "Account not found."
                    };
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<AccountDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the account."
                };
                return errorResponse;
            }
        }

        public async Task<ResponseDto<List<AccountShowDto>>> GetAccountByCustomerId(int customerId)
        {
            try
            {
                var accounts = await _context.Accounts.Where(account => account.CustomerId == customerId).ToListAsync();

                if (accounts != null && accounts.Any())
                {
                    var accountDtos = accounts.Select(account => _mapper.Map<AccountShowDto>(account)).ToList();
                    return new ResponseDto<List<AccountShowDto>>
                    {
                        Success = true,
                        Message = "Accounts Found",
                        Data = accountDtos
                    };
                }
                else
                {
                    return new ResponseDto<List<AccountShowDto>>
                    {
                        Success = false,
                        Message = "No accounts found for the given customer."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the account.");
                return new ResponseDto<List<AccountShowDto>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<ResponseDto<AccountDto>> AccountStatusChange(int AccountNo)
        {
            try
            {
                var accountObj = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == AccountNo);
                if (accountObj != null)
                {
                    accountObj.Status = !accountObj.Status;
                    await _context.SaveChangesAsync();

                    var responseDto = new ResponseDto<AccountDto>
                    {
                        Success = true,
                        Message = "Account Status Updated Successfully",

                    };

                    return responseDto;
                }
                else
                {
                    var errorResponse = new ResponseDto<AccountDto>
                    {
                        Success = false,
                        Message = "Account not found."
                    };
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errorResponse = new ResponseDto<AccountDto>
                {
                    Success = false,
                    Message = "Account not found."
                };
                return errorResponse;
            }
        }



       
    }
}
