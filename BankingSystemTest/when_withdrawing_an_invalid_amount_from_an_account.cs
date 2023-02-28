using BankingSystem;
using BankingSystem.AccountBanking;
using BankingSystem.UserData;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_withdrawing_an_invalid_amount_from_an_account
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string username = "first_user";

    private string accountName = "Checking";

    private WithdrawalTooLargeException exception;

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
            bankingHandler.WithdrawFromAccount(username, accountName, new DollarAmount(475m));
        }
        catch (WithdrawalTooLargeException ex)
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