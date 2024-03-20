namespace BankApplication.DTO
{
    public class AccountShowDto
    {
        public int AccountNo { set; get; }
        public int? CustomerId { get; set; }

        public string? AccountType { get; set; }

        public decimal? Balance { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public bool Status { get; set; }
    }
}
