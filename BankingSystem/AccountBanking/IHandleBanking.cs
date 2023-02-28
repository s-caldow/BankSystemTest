namespace BankingSystem.AccountBanking;

public interface IHandleBanking
{
    DollarAmount CheckBalance(string username, string accountName);

    void WithdrawFromAccount(string username, string accountName, DollarAmount amount);

    void DepositIntoAccount(string username, string accountName, DollarAmount amount);
}