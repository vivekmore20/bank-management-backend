namespace BankApplication.DTO
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int? AccountId { get; set; }

        public string? TransactionType { get; set; }

        public decimal? Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? Description { get; set; } 
    }
}
