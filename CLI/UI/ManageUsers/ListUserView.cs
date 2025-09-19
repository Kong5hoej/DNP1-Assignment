using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUserView (IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task GetOneUserAsync()
    {
        Console.WriteLine("What is the ID of the user?");
        int id =  Convert.ToInt32(Console.ReadLine());
        
        await userRepository.GetSingleUserAsync(id);
    }

    public IQueryable GetAllUsers()
    {
        return userRepository.GetManyUsers();
    }
    
    public async Task DeleteUserAsync()
    {
        Console.WriteLine("What is your username?");
        String? username = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? password = Console.ReadLine();
        try
        {
            User user = await FindUserAsync(username, password);
            await userRepository.DeleteUserAsync(user.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public async Task<User> FindUserAsync(string? username, string? password)
    {
        int id = -1;
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
        return await userRepository.GetSingleUserAsync(id);
    }
    
    public async Task<User> FindUserAsync(int userId)
    {
        int id = -1;
        for (int i = 0; i < userRepository.GetManyUsers().Count(); i++)
        {
            if (userId == userRepository.GetManyUsers().ElementAt(i).Id)
                    id = userRepository.GetManyUsers().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await userRepository.GetSingleUserAsync(id);
    }
}