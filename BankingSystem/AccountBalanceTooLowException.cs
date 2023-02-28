namespace BankingSystem;

public class AccountBalanceTooLowException : Exception
{
    public AccountBalanceTooLowException()
        : base("Account balance too low")
    {
    }
}