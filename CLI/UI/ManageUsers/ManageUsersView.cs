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
        await userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        await userRepository.DeleteUserAsync(user.Id);
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
    
    public async Task<User> FindUser(int userId)
    {
        return await listUserView.FindUserAsync(userId);
    }
    
    
}