using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = new List<User>();

    public Task<User> UserLogIn(String? username, String? password)
    {
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
        return Task.FromResult(returnUser);
    }
    
    public Task<User> AddUserAsync(User user)
    {
        user.Id = users.Any() 
            ? users.Max(u => u.Id) + 1
            : 1;

        for (int i = 0; i < users.Count; i++)
        {
            if (user.Username ==  users[i].Username)
                throw new Exception("Username already exists");
        }
        
        users.Add(user);
        return Task.FromResult(user);
    }
    
    public Task UpdateUserAsync(User user)
    {
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

        return Task.CompletedTask;
    }
    
    public Task DeleteUserAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }
    
    public Task<User> GetSingleUserAsync(int id)
    {
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        return Task.FromResult(user);
    }
    
    public IQueryable<User> GetManyUsers()
    {
        return users.AsQueryable();
    }
}