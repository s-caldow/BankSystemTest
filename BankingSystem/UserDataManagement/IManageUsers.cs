using BankingSystem.UserData;

namespace BankingSystem.UserDataManagement;

public interface IManageUsers
{
    void CreateNewUser(string username);

    IEnumerable<Account> GetUserAccounts(string username);

    void AddAccountsToUser(string username, params Account[] accounts);

    void RemoveAccountFromUser(string username, string accountName);
}