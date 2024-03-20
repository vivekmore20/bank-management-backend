using BankApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Repository
{
    public class InterestCalculateSavings:IInterestCalculatorRepo
    {
        private readonly BankDbContext _context;
        public InterestCalculateSavings(BankDbContext context)
        {
            _context = context;
        }
        public string AccountType { get { return "Savings"; } }

        public async Task<decimal> GetInterestRate()
        {
            try
            {
                var interest = await _context.Interests.FirstOrDefaultAsync(x => x.AccountType == AccountType);
                if (interest != null)
                {
                    return (decimal)interest.InterestRate;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving interest rate  \n{ex.Message}");
            }
            return -1;
        }
    }
}
