namespace BankingSystem.UserData;

public class User
{
    public User(string username)
    {
        this.Username = username;
        this.Accounts = new List<Account>();
    }

    public string Username { get; set; }

    public IEnumerable<Account> Accounts { get; set; }
}