using ApiContracts;
using ApiContracts.Users;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UserController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }
    
    //Create
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    { 
        try
        { 
            User user = new(request.Username, request.Password);
            User created = await userRepo.AddUserAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                Username = created.Username
            };
            return Created($"/Users/{dto.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto request)
    { 
        try
        {
            User user = await userRepo.GetSingleUserAsync(id);
            user.Username = request.Username;
            user.Password = request.Password;

            await userRepo.UpdateUserAsync(user);

            UserDto dto = new()
            {
                Id = user.Id,
                Username = user.Username
            };
            
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Get single
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetSingleUser(int id)
    { 
        try
        { 
            User user = await userRepo.GetSingleUserAsync(id);
            UserDto dto = new()
            {
                Id = user.Id,
                Username = user.Username
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //GetMany
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetManyUsers()
    { 
        try
        { 
            List<User> users = userRepo.GetManyUsers().ToList();
            
            var dtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            }).ToList();
            
            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> DeleteUser(int id)
    { 
        try
        { 
            await userRepo.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}