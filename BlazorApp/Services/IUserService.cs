using ApiContracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUser(CreateUserDto request);
    public Task<UserDto> UpdateUser(int id, UpdateUserDto request);
    public Task<GetSingleUserDto> GetSingleUser(int id);
    public Task<List<UserDto>> GetManyUsers();
    public Task <UserDto> DeleteUser(int id);
}