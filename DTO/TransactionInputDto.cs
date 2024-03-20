namespace BankApplication.DTO
{
    public class TransactionInputDto
    {
        public int? AccountId { get; set; }

        public string? TransactionType { get; set; }

        public decimal? Amount { get; set; }

        

        public string? Description { get; set; }
    }
}
