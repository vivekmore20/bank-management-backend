using BankApplication.DTO;

namespace BankApplication.Repository
{
    public interface ITransactionRepo
    {
        Task<ResponseDto<TransactionDto>> AddTransaction(TransactionInputDto transactionDto);
        Task<ResponseDto<List<TransactionDto>>> GetAllTransactions();
        Task<ResponseDto<List<TransactionDto>>> GetAlllTransactionsByAccountNumber(int AccountID);
        
    }
}
