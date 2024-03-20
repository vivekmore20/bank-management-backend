namespace BankApplication.DTO
{
    public class AccountDto
    {
       
        public int? CustomerId { get; set; }

        public string? AccountType { get; set; }

        public decimal? Balance { get; set; }

        public bool Status { get; set; }
    }
}
