using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraBankTask.DTO;
using TeraBankTask.Facade.Services;

namespace TeraBankTask.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [Route("getAllUser")]
    public IEnumerable<User> GetAll()
    {
        return userService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var user = userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut]
    [Route("updateUser")]
    public IActionResult Update(User user)
    {
        if (user == null)
        {
            return NotFound();
        }

        try
        {
            userService.Update(user);
            return Ok("Updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("deleteUserById")]
    public IActionResult Delete(int id)
    {
        var user = userService.GetById(id);

        if (user == null)
        {
            return NotFound();
        }

        try
        {
            userService.Delete(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}