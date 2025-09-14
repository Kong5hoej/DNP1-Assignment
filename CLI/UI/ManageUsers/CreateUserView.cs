using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView (IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<User> AddUserAsync(string? username, string? password)
    {
        User user = new User(username, password);
        return await userRepository.AddUserAsync(user) ??  throw new Exception($"User creation failed, try again!");
    }
}