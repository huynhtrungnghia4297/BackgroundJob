using BackgroundJob;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] 
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public  UsersController (UserRepository userRepository)
    {
        _userRepository = userRepository;   
    }

    [HttpGet("status")] 
    public IActionResult GetUsersStatus()
    {
        var users = _userRepository.GetUsers();
        return Ok(users);       
    }
}
