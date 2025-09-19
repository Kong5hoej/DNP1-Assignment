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
        
        return await userRepository.UserLogIn(username, password);
    }

    public async Task<User> AddUserAsync()
    {
        Console.WriteLine("What will the username be?");
        String? username = Console.ReadLine();
        Console.WriteLine("What will the password be?");
        String? password = Console.ReadLine();
        User user = new User(username, password);
        try
        {
            await userRepository.AddUserAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine("Username is already in use. Enter a different username: ");
            username = Console.ReadLine();
            user.Username = username;
            await userRepository.AddUserAsync(user);
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