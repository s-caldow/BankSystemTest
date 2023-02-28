using System.Linq;
using BankingSystem;
using BankingSystem.UserData;
using BankingSystem.UserDataManagement;
using FluentAssertions;
using NUnit.Framework;

namespace BankingSystemTest;

public class Tests
{
    private DatabaseManager _databaseManager = new DatabaseManager();

    [SetUp]
    public void Setup()
    {
        var dataManager = new UserDataManager(_databaseManager);
        dataManager.CreateNewUser("first_user");
    }

    [Test]
    public void then_a_new_user_should_be_added()
    {
        _databaseManager.GetUsers().Count().Should().Be(1);
    }

    [Test]
    public void then_the_user_should_be_correct()
    {
        _databaseManager.GetUsers().First().Should().BeEquivalentTo(new User("first_user"));
    }
}