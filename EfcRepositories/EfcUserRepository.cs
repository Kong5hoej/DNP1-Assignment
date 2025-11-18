using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddUserAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateUserAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new Exception($"User with id {user.Id} not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new Exception($"User with id {id} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        User? user = ctx.Users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetManyUsers()
    {
        return ctx.Users.AsQueryable();
    }

    public async Task<User> UserLogIn(string? username, string? password)
    {
        List<User> users = ctx.Users.AsQueryable().ToList();
        
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

        if (returnUser is null)
            throw new UnauthorizedAccessException("You don't have permission to log in");
        
        await ctx.SaveChangesAsync();
        return returnUser;
    }
}