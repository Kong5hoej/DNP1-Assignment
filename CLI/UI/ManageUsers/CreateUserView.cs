using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView (IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<User> UserLogIn()
    {
        Console.WriteLine("What is your username?");
        String? username = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? password = Console.ReadLine();
        
        try
        {
            User user = await userRepository.UserLogIn(username, password);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        throw new Exception("User is not found!");
    }

    public async Task<User> AddUserAsync()
    {
        User? user = null;

        while (user == null)
        {
            Console.WriteLine("What will the username be?");
            string? username = Console.ReadLine();

            if (userRepository.GetManyUsers().Any(u => u.Username == username))
            {
                Console.WriteLine("Username is already in use. Enter a different username.");
                continue; 
            }

            Console.WriteLine("What will the password be?");
            string? password = Console.ReadLine();

            User tempUser = new User(username, password);
            user = await userRepository.AddUserAsync(tempUser);
        }

        Console.WriteLine("The user has now been created!");
        return user;
    }

    public async Task UpdateUserAsync()
    {
        Console.WriteLine("What is your username?");
        String? username = Console.ReadLine();
        Console.WriteLine("What is your password?");
        String? password = Console.ReadLine();
        User user = new User(username, password);
        try
        {
            await userRepository.UpdateUserAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("The user has now been updated!");
    }
}