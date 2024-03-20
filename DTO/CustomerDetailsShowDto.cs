namespace BankApplication.DTO
{
    public class CustomerDetailsShowDto
    {

        public int CustomerId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
        public int AccountNo { get; set; }


        public string? AadharNumber { get; set; }

        public string? AccountType { get; set; }

        public decimal? Balance { get; set; }

        public DateTime? AccountOpenDate { get; set; }

        public bool Status { get; set; }
    }
}
