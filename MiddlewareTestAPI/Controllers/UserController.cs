using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddlewareTestAPI.Models;

namespace MiddlewareTestAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult GetUserInfo(int id)
    {
        _logger.LogInformation("Hello from Get User Info");
        var user = new UserLoginResponseModel()
        {
            Success = true,
            UserEmail = "abc@gmail.com",
        };

        return Ok(user);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLoginRequestModel model)
    {
        _logger.LogInformation("Hello from Login");
        var user = new UserLoginResponseModel()
        {
            Success = true,
            UserEmail = "abc@gmail.com",
        };

        return Ok(user);
    }

    [HttpPost]
    [Route("loginonly")]
    public IActionResult LoginOnly([FromBody] UserLoginRequestModel model)
    {
        _logger.LogInformation("Hello from Login Only");

        return Ok();
    }

}
