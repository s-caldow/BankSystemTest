using System.Linq;
using BankingSystem;
using BankingSystem.UserData;
using BankingSystem.UserDataManagement;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_adding_accounts_to_a_user___and_account_value_is_too_small
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string userName = "first_user";

    private AccountBalanceTooLowException exception;

    [SetUp]
    public void Setup()
    {
        var dataManager = new UserDataManager(_databaseManager);
        dataManager.CreateNewUser(userName);
        dataManager.CreateNewUser("other_user");

        try
        {
            dataManager.AddAccountsToUser(
                userName,
                new Account
                {
                    AccountName = "Checking",
                    Amount = new DollarAmount(50m)
                },
                new Account
                {
                    AccountName = "Savings",
                    Amount = new DollarAmount(150.14m)
                });
        }
        catch (AccountBalanceTooLowException ex)
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