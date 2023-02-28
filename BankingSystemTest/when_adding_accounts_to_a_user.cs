using System.Linq;
using BankingSystem;
using BankingSystem.UserData;
using BankingSystem.UserDataManagement;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class when_adding_acocunts_to_a_user
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
                AccountName = "Fun Money",
                Amount = new DollarAmount(100000m)
            });
    }

    [Test]
    public void then_the_accounts_are_added_to_the_database()
    {
        _databaseManager.GetAccountsByUser("first_user").Select(o => o.AccountName)
            .Should().BeEquivalentTo(new[]
        {
            "Checking",
            "Savings"
        });
    }
}