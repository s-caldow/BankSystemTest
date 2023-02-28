using BankingSystem.UserData;

namespace BankingSystem;

public class DatabaseManager
{
    private List<DatabaseUser> userTable;

    private List<DatabaseAccount> accountTable;

    public DatabaseManager()
    {
        userTable = new List<DatabaseUser>();
        accountTable = new List<DatabaseAccount>();
    }

    public IEnumerable<User> GetUsers()
    {
        return userTable.Select(o => new User(o.Username)
        {
            Accounts = accountTable
                .Where(da => da.Username.Equals(o.Username))
                .Select(a => new Account
                {
                    AccountName = a.AccountName,
                    Amount = new DollarAmount(a.Value),
                })
        });
    }

    public IEnumerable<Account> GetAccounts()
    {
        return accountTable.Select(o => new Account
        {
            AccountName = o.AccountName,
            Amount = new DollarAmount(o.Value),
        });
    }

    public User GetUser(string username)
    {
        var user = this.userTable.First(o => o.Username.Equals(username));
        return new User(user.Username)
        {
            Accounts = this.GetAccountsByUser(user.Username)
        };
    }

    public void AddUser(User user)
    {
        this.userTable.Add(new DatabaseUser { Username = user.Username });
    }

    public IEnumerable<Account> GetAccountsByUser(string username)
    {
        return accountTable.Where(o => o.Username.Equals(username)).Select(o => new Account
        {
            AccountName = o.AccountName,
            Amount = new DollarAmount(o.Value)
        });
    }

    public Account GetAccountByUser(string username, string accountName)
    {
        return accountTable.Where(o => o.Username.Equals(username) && o.AccountName.Equals(accountName))
            .Select(o => new Account
        {
            AccountName = o.AccountName,
            Amount = new DollarAmount(o.Value)
        }).Single();
    }

    public void AddAccounts(string username, IEnumerable<Account> accounts)
    {
        this.accountTable.AddRange(accounts.Select(o => new DatabaseAccount
        {
            Username = username,
            AccountName = o.AccountName,
            Value = o.Amount.Value,
        }));
    }

    public void DeleteAccount(string username, string accountName)
    {
        var accountToRemove =
            this.accountTable.Find(
                o => o.Username == username && o.AccountName == accountName);
        if (accountToRemove != null)
        {
            this.accountTable.Remove(accountToRemove);
        }
    }

    public void UpdateAccountValue(string username, string accountName, DollarAmount updatedAmount)
    {
        var accountToUpdate = this.accountTable.Find(
            o => o.Username == username && o.AccountName == accountName);

        if (accountToUpdate != null)
        {
            accountToUpdate.Value = updatedAmount.Value;
        }
    }
}
