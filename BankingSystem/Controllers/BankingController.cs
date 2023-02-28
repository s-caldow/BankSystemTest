using BankingSystem.AccountBanking;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class BankingController : ControllerBase
{
    private IHandleBanking bankingManager;

    public BankingController(IHandleBanking bankingManager)
    {
        this.bankingManager = bankingManager;
    }

    [HttpGet("{username}/CheckBalance/{accountId}")]
    public IActionResult CheckBalance(string username, string accountId)
    {
        return this.Ok(this.bankingManager.CheckBalance(username, accountId));
    }

    [HttpPost("{username}/Deposit/{accountId}")]
    public IActionResult Deposit(string username, string accountId, [FromBody]DollarAmount amount)
    {
        try
        {
            this.bankingManager.DepositIntoAccount(username, accountId, amount);
            return this.Ok();
        }
        catch (DepositTooLargeException ex)
        {
            return this.BadRequest("Deposit too large");
        }
    }

    [HttpPost("{username}/Withdraw/{accountId}")]
    public IActionResult Withdraw(string username, string accountId, [FromBody]DollarAmount amount)
    {
        try
        {
            this.bankingManager.WithdrawFromAccount(username, accountId, amount);
            return this.Ok();
        }
        catch (WithdrawalTooLargeException ex)
        {
            return this.BadRequest("Withdrawal too large");
        }
        catch (AccountBalanceTooLowException ex)
        {
            return this.BadRequest("Balance too low to complete withdrawal");
        }
    }
}