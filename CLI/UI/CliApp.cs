using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp( 
    IUserRepository userRepository,
    ICommentRepository commentRepository,
    IPostRepository postRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly IPostRepository postRepository = postRepository;
    
    private ManageUsersView manageUsersView = new ManageUsersView(userRepository);

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("What do you want to do? " +
                              "\n 1. Create a new user " +
                              "\n 2. Update an user" +
                              "\n 3. List one user " +
                              "\n 4. List all users " +
                              "\n 5. Delete an user");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("What will the username be?");
                    String? username1 = Console.ReadLine();
                    Console.WriteLine("What will the password be?");
                    String? password1 = Console.ReadLine();
                    try
                    {
                        await manageUsersView.AddUserAsync(username1, password1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Username is already in use. Enter a different username: ");
                        username1 = Console.ReadLine();
                        await manageUsersView.AddUserAsync(username1, password1);
                    }
                    Console.WriteLine("The user has now been created!");
                    break;
                case 2:
                    Console.WriteLine("What is your username?");
                    String? username2 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password2 = Console.ReadLine();
                    User user2 = await manageUsersView.FindUser(username2, password2);
                    await manageUsersView.UpdateUserAsync(user2);
                    Console.WriteLine("The user has now been updated!");
                    break;
                case 3:
                    Console.WriteLine("What is your username?");
                    String? username3 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password3 = Console.ReadLine();
                    User user3 = await manageUsersView.FindUser(username3, password3);
                    await manageUsersView.GetOneUserAsync(user3.Id);
                    Console.WriteLine($"The user has now been retrieved! Your username is {user3.Username} and your ID is {user3.Id}");
                    break;
                case 4:
                    foreach (User user4 in manageUsersView.GetAllUsers())
                    {
                        Console.WriteLine($"{user4.Id}: {user4.Username}");
                    }
                    break;
                case 5:
                    Console.WriteLine("What is your username?");
                    String? username5 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password5 = Console.ReadLine();
                    User user5 = await manageUsersView.FindUser(username5, password5);
                    await manageUsersView.DeleteUserAsync(user5);
                    break;
            }
        }
    }
}
