using BankingSystem.UserData;
using BankingSystem.UserDataManagement;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountManagementController : ControllerBase
{
    private IManageUsers userManager;

    public AccountManagementController(IManageUsers userManager)
    {
        this.userManager = userManager;
    }

    [HttpPut(Name = "AddUserWithAccounts")]
    public IActionResult AddUser(User user)
    {
        try
        {
            this.userManager.CreateNewUser(user.Username);
            this.userManager.AddAccountsToUser(user.Username, user.Accounts.ToArray());

            return this.Ok();
        }
        catch (AccountBalanceTooLowException ex)
        {
            return this.BadRequest("Balance too low to create an account");
        }
    }

    [HttpPost("AddAccount/{username}")]
    public IActionResult AddAccount(string username, [FromBody]Account accounts)
    {
        try
        {
            this.userManager.AddAccountsToUser(username, accounts);

            return this.Ok();
        }
        catch (AccountBalanceTooLowException ex)
        {
            return this.BadRequest("Balance too low to create an account");
        }
    }

    [HttpDelete("DeleteAccount/{username}/{accountName}")]
    public IActionResult DeleteAccount(string username, string accountName)
    {
        this.userManager.RemoveAccountFromUser(username, accountName);

        return this.Ok();
    }
}