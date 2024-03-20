namespace BankApplication.DTO
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string? AadharNumber { get; set; }

    }
}
