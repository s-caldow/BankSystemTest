using BankingSystem;
using BankingSystem.AccountBanking;
using BankingSystem.UserData;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_withdrawing_a_valid_amount_from_an_account
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string username = "first_user";

    private string accountName = "Checking";

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
            }});

        var bankingHandler = new BankingHandler(_databaseManager);
        bankingHandler.WithdrawFromAccount(username, accountName, new DollarAmount(25m));
    }

    [Test]
    public void then_the_balance_should_be_correct()
    {
        this._databaseManager.GetAccountByUser(username, accountName).Amount.Value.Should().Be(475m);
    }
}