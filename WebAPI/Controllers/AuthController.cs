using ApiContracts.Users;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  private readonly IUserRepository userRepo;

  public AuthController(IUserRepository userRepo)
  {
    this.userRepo = userRepo;
  }
  
  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginRequestDto dto)
  { 
    try
    { 
      User user = await userRepo.UserLogIn(dto.Username, dto.Password);
      UserDto userDto = new()
      {
        Id = user.Id,
        Username = user.Username
      };
      return Ok(userDto);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
}