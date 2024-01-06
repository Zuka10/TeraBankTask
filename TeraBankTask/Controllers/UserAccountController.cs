using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraBankTask.Facade.Services;

namespace TeraBankTask.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserAccountController : Controller
{
    private readonly IUserAccountService userAccountService;

    public UserAccountController(IUserAccountService userAccountService)
    {
        this.userAccountService = userAccountService;
    }

    [HttpPost]
    [Route("deposit")]
    public IActionResult Deposit(string accountNumber, decimal amount)
    {
        try
        {
            userAccountService.Deposit(accountNumber, amount);
            return Ok("Deposit made succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("withdraw")]
    public IActionResult Withdraw(string accountNumber, decimal amount)
    {
        try
        {
            userAccountService.Withdraw(accountNumber, amount);
            return Ok("withdrawal made successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("transfer")]
    public IActionResult Transfer(string senderAccountNumber, string recepientAccountNumber, decimal amount)
    {
        try
        {
            userAccountService.Transfer(senderAccountNumber, recepientAccountNumber, amount);
            return Ok("withdrawal made successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("getaccount")]
    public IActionResult GetAccount(int userId)
    {
        try
        {
            var userAccount = userAccountService.GetAccount(userId);
            if(userAccount == null)
            {
                return NotFound();
            }

            return Ok(userAccount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
