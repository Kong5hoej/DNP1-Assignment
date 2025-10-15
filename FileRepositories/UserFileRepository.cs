using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "user.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<User> AddUserAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        user.Id = users.Any() 
            ? users.Max(u => u.Id) + 1
            : 1;
        
        if (users.Contains(user))
            return null;
        
        users.Add(user);
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteUserAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public IQueryable<User> GetManyUsers()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }

    public async Task<User> UserLogIn(string? username, string? password)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User returnUser = null;
        if (username == null || password == null)
            throw new Exception("User is not created");
        foreach (User user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                returnUser = user;
                break;
            }
        }
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return returnUser;
    }
}