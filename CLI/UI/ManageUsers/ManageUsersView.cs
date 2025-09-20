using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView(IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly CreateUserView createUserView = new CreateUserView(userRepository);
    private readonly ListUserView listUserView = new ListUserView(userRepository);

    public async Task<User> BeforeAsync()
    {
        User user = null;
        while (user == null)
        {
            Console.WriteLine("\n What do you want to do?" +
                              "\n 1. Log in" +
                              "\n 2. Create a new user");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch (userChoice)
            {
                case 1:
                    user = await createUserView.UserLogIn();
                    if (user == null)
                    {
                        Console.WriteLine("Incorrect credentials, try again!");
                    }
                    break;
                case 2:
                    user = await createUserView.AddUserAsync();
                    break;
            }
        }

        return user;
        
    }
    
    public async Task StartAsync()
    {
        Console.WriteLine("What do you want to manage?"+
                          "\n 1. Update an user" +
                          "\n 2. List one user " +
                          "\n 3. List all users " +
                          "\n 4. Delete an user" +
                          "\n 5. Exit");
        int choice = Convert.ToInt32(Console.ReadLine());
        
        if (choice == 5)
        {
            return;
        }

        switch (choice)
        {
            case 1:
                await createUserView.UpdateUserAsync();
                break;
            case 2:
                await listUserView.GetOneUserAsync();
                break;
            case 3:
                listUserView.GetAllUsers();
                break;
            case 4:
                await listUserView.DeleteUserAsync();
                break;
        }
    }

    public async Task<User> FindUser(string? username, string? password)
    {
        return await listUserView.FindUserAsync(username, password);
    }
    
    public async Task<User> FindUser(int id)
    {
        return await listUserView.FindUserAsync(id);
    }
}