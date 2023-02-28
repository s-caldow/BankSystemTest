using BankingSystem.UserData;

namespace BankingSystem.UserDataManagement;

public class UserDataManager : IManageUsers
{
    private DatabaseManager _databaseManager;

    public UserDataManager(DatabaseManager databaseManager)
    {
        this._databaseManager = databaseManager;
    }

    public void CreateNewUser(string username)
    {
        this._databaseManager.AddUser(new User(username));
    }

    public IEnumerable<Account> GetUserAccounts(string username)
    {
        return this._databaseManager.GetAccountsByUser(username);
    }

    public void AddAccountsToUser(string username, params Account[] accounts)
    {
        if (accounts.Any(o => o.Amount.Value < AccountBalanceLimits.AccountBalanceMinimum))
        {
            throw new AccountBalanceTooLowException();
        }

        this._databaseManager.AddAccounts(username, accounts);
    }

    public void RemoveAccountFromUser(string username, string accountName)
    {
        this._databaseManager.DeleteAccount(username, accountName);
    }
}