using BankApplication.DTO;

namespace BankApplication.Repository
{
    public interface IAccountRepo
    {
        Task<ResponseDto<AccountDto>> AddAccount(AccountDto accountDto);

        Task<ResponseDto<List<AccountShowDto>>> GetAllAccounts();
        Task<ResponseDto<AccountDto>> DeleteAccount(int id);
        Task<ResponseDto<AccountDto>> UpdateAccount(int id, AccountDto accountDto);
        Task<ResponseDto<AccountDto>> AccountStatusChange(int AccountNo);

        Task<ResponseDto<List<AccountShowDto>>> GetAccountByCustomerId(int customerId);
    }
}
