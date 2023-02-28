namespace BankingSystem.AccountBanking;

public class WithdrawalTooLargeException : Exception
{
    public WithdrawalTooLargeException()
        : base("Withdrawal more than acceptable percentage of account contents")
    {
    }
}