using BankingSystem;
using BankingSystem.AccountBanking;
using BankingSystem.UserData;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_withdrawing_an_amount_that_drops_below_the_balance_threshold
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string username = "first_user";

    private string accountName = "Checking";

    private AccountBalanceTooLowException exception;

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
                Amount = new DollarAmount(101m)
            }});

        var bankingHandler = new BankingHandler(_databaseManager);
        try
        {
            bankingHandler.WithdrawFromAccount(username, accountName, new DollarAmount(5m));
        }
        catch (AccountBalanceTooLowException ex)
        {
            this.exception = ex;
        }

    }

    [Test]
    public void then_an_exception_should_be_thrown()
    {
        this.exception.Should().NotBeNull();
    }
}