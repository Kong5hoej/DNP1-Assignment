using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUserView (IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<User> GetOneUserAsync(int id)
    {
        Console.WriteLine("Searching for single user...");
        return await userRepository.GetSingleUserAsync(id);
    }
    
    public IQueryable<User> GetAllUsers()
    {
        Console.WriteLine("Searching...");
        return userRepository.GetManyUsers();
    }
    
    public async Task<User> FindUserAsync(string? username, string? password)
    {
        int id = -1;
        Console.WriteLine("Finding user...");
        for (int i = 0; i < userRepository.GetManyUsers().Count(); i++)
        {
            if (username == userRepository.GetManyUsers().ElementAt(i).Username)
                if  (password == userRepository.GetManyUsers().ElementAt(i).Password)
                    id = userRepository.GetManyUsers().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await GetOneUserAsync(id);
    }
}