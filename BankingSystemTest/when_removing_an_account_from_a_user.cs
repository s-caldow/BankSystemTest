using System.Linq;
using BankingSystem;
using BankingSystem.UserData;
using BankingSystem.UserDataManagement;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_removing_an_account_from_a_user
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    private string userName = "first_user";

    [SetUp]
    public void Setup()
    {
        var dataManager = new UserDataManager(_databaseManager);
        dataManager.CreateNewUser(userName);
        dataManager.CreateNewUser("other_user");

        dataManager.AddAccountsToUser(
            userName,
            new Account
            {
                AccountName = "Checking",
                Amount = new DollarAmount(500m)
            },
            new Account
            {
                AccountName = "Savings",
                Amount = new DollarAmount(150.14m)
            });

        dataManager.AddAccountsToUser(
            "other_user",
            new Account
            {
                AccountName = "Savings",
                Amount = new DollarAmount(100000m)
            });

        dataManager.RemoveAccountFromUser(userName, "Savings");
    }

    [Test]
    public void then_the_account_is_removed_from_the_user()
    {
        _databaseManager.GetAccountsByUser(userName).Select(o => o.AccountName).Should().NotContain("Savings");
    }

    [Test]
    public void then_other_accounts_with_the_same_name_are_not_removed()
    {
        _databaseManager.GetAccountsByUser("other_user").Select(o => o.AccountName).Should().Contain("Savings");
    }
}