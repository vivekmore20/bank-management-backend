namespace BankApplication.Repository
{
    public interface IInterestCalculatorRepo
    {
        string AccountType { get; }
        Task<decimal> GetInterestRate();
    }
}
