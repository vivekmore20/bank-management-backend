using AutoMapper;
using BankApplication.DTO;
using BankApplication.Models;
using BankManagement.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Repository
{
    public class TransactionRepo:ITransactionRepo
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMapper _mapper;
        private readonly BankDbContext  _context;

        public TransactionRepo(ILogger<TransactionController> logger, IMapper mapper, BankDbContext bankDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _context = bankDbContext;
        }

        public async Task<ResponseDto<TransactionDto>> AddTransaction(TransactionInputDto transactionDto)
        {
            try
            {
                var obj = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == transactionDto.AccountId);

                if (obj != null)
                {
                    if (obj.Status && transactionDto.TransactionType == "Withdraw")
                    {
                        if (obj.Balance >= transactionDto.Amount)
                        {
                            obj.Balance -= transactionDto.Amount;
                           
                            var response = new ResponseDto<TransactionDto>
                            {
                                Success = true,
                                Message = "Withdrawal successful",
                            };
                            var t1= _mapper.Map<Transaction>(transactionDto);
                            if(t1 != null)
                            {
                                t1.TransactionDate=DateTime.Now;
                                _context.Transactions.Add(t1);
                                await _context.SaveChangesAsync();

                            }
                            
                            return response;
                        }
                        else
                        {
                            var response = new ResponseDto<TransactionDto>
                            {
                                Success = false,
                                Message = "Insufficient funds for withdrawal",
                            };
                            return response;
                        }
                    }
                    else if (obj.Status && transactionDto.TransactionType == "Deposit")
                    {
                        obj.Balance += transactionDto.Amount;
                       
                        var response = new ResponseDto<TransactionDto>
                        {
                            Success = true,
                            Message = "Deposit successful",
                        };
                        var t1 = _mapper.Map<Transaction>(transactionDto);
                        if (t1 != null)
                        {
                            t1.TransactionDate= DateTime.Now;
                            _context.Transactions.Add(t1);
                            await _context.SaveChangesAsync();

                        }
                        return response;
                    }
                    else
                    {
                        var response = new ResponseDto<TransactionDto>
                        {
                            Success = false,
                            Message = "Invalid transaction type or account status",
                        };
                        return response;
                    }
                }
                else
                {

                    var response = new ResponseDto<TransactionDto>
                    {
                        Success = false,
                        Message = "Invalid account number"
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex}");
                var errorResponseDto = new ResponseDto<TransactionDto>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                };
                return errorResponseDto;
            }

            
        }

        public async Task<ResponseDto<List<TransactionDto>>> GetAllTransactions()
        {
            try
            {
                var transactions = await _context.Transactions.FromSqlRaw("GetAllTransactions").ToListAsync();
                var transactionsList=_mapper.Map<List<TransactionDto>>(transactions);
                var response = new ResponseDto<List<TransactionDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data= transactionsList
                };
                return response;

            }catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                var errorResponse = new ResponseDto<List<TransactionDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching transactions.",
                    Data = null
                };

                return errorResponse;
            }
        }
        public async Task<ResponseDto<List<TransactionDto>>> GetAlllTransactionsByAccountNumber(int AccountID)
        {
            try
            {
                var transactions = await _context.Transactions.FromSqlRaw("GetAllTransactionsByAccountId @AccountID", new SqlParameter("@AccountID",AccountID)).ToListAsync();
                var transactionsList = _mapper.Map<List<TransactionDto>>(transactions);
                var response = new ResponseDto<List<TransactionDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = transactionsList
                };
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                var errorResponse = new ResponseDto<List<TransactionDto>>
                {
                    Success = false,
                    Message = "An error occurred while fetching transactions.",
                    Data = null
                };

                return errorResponse;
            }
        }

        



    }
}
