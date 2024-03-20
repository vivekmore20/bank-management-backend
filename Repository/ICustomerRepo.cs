using BankApplication.DTO;

namespace BankApplication.Repository
{
    public interface ICustomerRepo
    {
        Task<ResponseDto<CustomerDto>> AddCustomer(CustomerInputDto customerInputDto);

        Task<ResponseDto<List<CustomerDto>>> GetAllCustomers();

        Task<ResponseDto<List<CustomerDetailsShowDto>>> GetAllCustomerWithAccountDetails();

        Task<ResponseDto<CustomerDto>> DeleteCustomer(string aadharNumber);
        Task<ResponseDto<CustomerDto>> UpdateCustomer(int id,CustomerInputDto customerInputDto);

        Task<ResponseDto<CustomerDetailsShowDto>> GetCustomerWithAccountDetailsByAadharNo(String AadharNumber);

        Task<ResponseDto<CustomerDetailsShowDto>> GetCustomerWithAccountDetailsBycustomerId(int CustomerId);
    }
}
