using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MyHabitTrackerApp.Models;
using MyHabitTrackerApp.Repositories;
namespace backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repository;
    public AccountController(IAccountRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(Register model)
    {
        var user = new User { UserName = model.UserName, Email = model.Email };

        var result = await _repository.RegisterAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(Login model)
    {
        var result = await _repository.LoginAsync(model.UserName, model.Password, model.RememberMe);

        if (result.Succeeded)
        {
            return Ok();
        }
        
        return Unauthorized();
    
    }
}
