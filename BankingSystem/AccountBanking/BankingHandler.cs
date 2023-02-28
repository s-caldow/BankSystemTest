namespace BankingSystem.AccountBanking;

public class BankingHandler : IHandleBanking
{
    private DatabaseManager _databaseManager;

    public BankingHandler(DatabaseManager databaseManager)
    {
        this._databaseManager = databaseManager;
    }

    public DollarAmount CheckBalance(string username, string accountName)
    {
        var account = this._databaseManager.GetAccountByUser(username, accountName);
        return account.Amount;
    }

    public void WithdrawFromAccount(string username, string accountName, DollarAmount amount)
    {
        var account = this._databaseManager.GetAccountByUser(username, accountName);

        if (amount.Value > (account.Amount.Value * AccountBalanceLimits.WithdrawalPercentageLimit))
        {
            throw new WithdrawalTooLargeException();
        }
        else if ((account.Amount.Value - amount.Value) < AccountBalanceLimits.AccountBalanceMinimum)
        {
            throw new AccountBalanceTooLowException();
        }

        this._databaseManager.UpdateAccountValue(username, accountName, account.Amount.Subtract(amount));
    }

    public void DepositIntoAccount(string username, string accountName, DollarAmount amount)
    {
        var account = this._databaseManager.GetAccountByUser(username, accountName);
        if (amount.Value > AccountBalanceLimits.DepositAmountLimit)
        {
            throw new DepositTooLargeException();
        }

        this._databaseManager.UpdateAccountValue(username, accountName, account.Amount.Add(amount));
    }
}