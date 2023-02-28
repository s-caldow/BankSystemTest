using BankingSystem;
using BankingSystem.AccountBanking;
using BankingSystem.UserData;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_checking_an_account_balance
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string username = "first_user";

    private string accountName = "Checking";

    private DollarAmount accountBalance;

    [SetUp]
    public void Setup()
    {
        this._databaseManager.AddUser(new User(username));
        this._databaseManager.AddAccounts(
            username,
            new[] {
                    new Account
            {
                AccountName = "Checking",
                Amount = new DollarAmount(500m)
            },
            new Account
            {
                AccountName = "Savings",
                Amount = new DollarAmount(150.14m)
            }
            });

        var bankingHandler = new BankingHandler(_databaseManager);
        accountBalance = bankingHandler.CheckBalance(username, accountName);
    }

    [Test]
    public void then_the_balance_should_be_correct()
    {
        this.accountBalance.Value.Should().Be(500m);
    }
}