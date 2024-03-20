using AutoMapper;
using BankApplication.DTO;
using BankApplication.Models;

namespace BankManagement.Helper
{
    public class ApplicationModelMapping : Profile
    {
        public ApplicationModelMapping()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerInputDto>().ReverseMap();
            CreateMap<AccountDto,CustomerInputDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
          
            CreateMap<Account, CustomerInputDto>().ReverseMap();
            CreateMap<Transaction,TransactionDto>().ReverseMap();
            CreateMap<Transaction, TransactionInputDto>().ReverseMap();
            CreateMap<AccountShowDto, Account>().ReverseMap();
        }
    }
}
