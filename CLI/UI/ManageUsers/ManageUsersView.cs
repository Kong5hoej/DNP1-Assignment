using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView(IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly CreateUserView createUserView = new CreateUserView(userRepository);
    private readonly ListUserView listUserView = new ListUserView(userRepository);
    public async Task UpdateUserAsync(User user)
    { 
        Console.WriteLine("Updating user...");
        await userRepository.UpdateUserAsync(user);
        Console.WriteLine("User updated");
    }

    public async Task DeleteUserAsync(User user)
    {
        Console.WriteLine("Deleting user...");
        await userRepository.DeleteUserAsync(user.Id);
        Console.WriteLine("User deleted");
    }

    public async Task<User> AddUserAsync(string? username, string? password)
    {
        return await createUserView.AddUserAsync(username, password);
    }

    public async Task<User> GetOneUserAsync(int id)
    {
        return await listUserView.GetOneUserAsync(id);
    }

    public IQueryable<User> GetAllUsers()
    {
        return listUserView.GetAllUsers();
    }

    public async Task<User> FindUser(string? username, string? password)
    {
        return await listUserView.FindUserAsync(username, password);
    }
    
    
}