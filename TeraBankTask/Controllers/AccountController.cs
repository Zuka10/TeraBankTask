using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraBankTask.API.Modules;
using TeraBankTask.Controllers;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Services;

namespace TeraBankTask.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly ILogger<UserController> logger;

    public AccountController(IUserService userService, ITokenService tokenService, ILogger<UserController> logger)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.logger = logger;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public IActionResult Create([FromForm] RegisterViewModel registerViewModel)
    {
        try
        {
            var user = new User
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password
            };

            if (userService.IsValidEmail(user.Email))
            {
                userService.Create(user);
                return Ok("Created successfully");
            }
            else
            {
                return BadRequest("Email is in invalid format");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public IActionResult Login([FromForm] LoginViewModel loginViewModel)
    {
        try
        {
            var authenticatedCustomer = userService.Authenticate(loginViewModel.Email, loginViewModel.Password);

            if (authenticatedCustomer != null)
            {
                var token = tokenService.GenerateToken(authenticatedCustomer);

                return Ok("Bearer " + token);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }

        return BadRequest("Email or password is incorrect");
    }
}