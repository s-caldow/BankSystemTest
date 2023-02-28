namespace BankingSystem.AccountBanking;

public class DepositTooLargeException : Exception
{
    public DepositTooLargeException()
        : base("Deposit exceeds maximum deposit")
    {
    }
}