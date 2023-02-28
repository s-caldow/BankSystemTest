using BankingSystem;
using BankingSystem.AccountBanking;
using BankingSystem.UserData;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_depositing_a_too_large_amount_to_an_account
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string username = "first_user";

    private string accountName = "Checking";

    private DepositTooLargeException exception;

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
        try
        {
            bankingHandler.DepositIntoAccount(username, accountName, new DollarAmount(10001m));
        }
        catch (DepositTooLargeException ex)
        {
            this.exception = ex;
        }

    }

    [Test]
    public void then_an_exception_is_thrown()
    {
        this.exception.Should().NotBeNull();
    }
}