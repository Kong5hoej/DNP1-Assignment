using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
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
    private ManagePostsView managePostsView = new ManagePostsView(postRepository, commentRepository, userRepository);
    private ManageCommentsView manageCommentsView = new ManageCommentsView(commentRepository);

    public async Task StartAsync()
    {
        initialiseDummyData();
        User user = await manageUsersView.BeforeAsync();
        
        while (user != null)
        {
            Console.WriteLine("\n What do you want to do? " +
                              "\n 1. User management:" +
                              "\n 2. Post management:" +
                              "\n 3. Comment management:" +
                              "\n 4. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 4)
            {
                break;
            }

            switch (choice)
            {
                case 1:
                    await manageUsersView.StartAsync();
                    break;
                case 2:
                    await managePostsView.StartAsync(user);
                    break;
                case 3:
                    await manageCommentsView.StartAsync(user);
                    break;
            }
        }
    }

    private void initialiseDummyData()
    {
        postRepository.DummyData();
        commentRepository.DummyData();
    }
}
