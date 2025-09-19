using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

/**
 * TO-DO:
 * Tjek at man kan logge ind osv.
 * Tilret start i manageUsersView
 * Virker ListPostsView.cs i linje 17-18, når man vil vise kommentarene - få den til at vise brugernavn fremfor brugerID
 * Kør kun det store while-loop mens man er logget ind
 * Lav ny dummydata
 * Tjek at det hele virker, og så lav exercise 3
 */

public class CliApp( 
    IUserRepository userRepository,
    ICommentRepository commentRepository,
    IPostRepository postRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly IPostRepository postRepository = postRepository;
    
    private ManageUsersView manageUsersView = new ManageUsersView(userRepository);
    private ManagePostsView managePostsView = new ManagePostsView(postRepository, commentRepository);
    private ManageCommentsView manageCommentsView = new ManageCommentsView(commentRepository);

    public async Task StartAsync()
    {
        initialiseDummyData();
        User user = await manageUsersView.BeforeAsync();
        //await manageUsersView.StartAsync();
        
        while (true)
        {
            Console.WriteLine("\n What do you want to do? " +
                              "\n 1. User management:" +
                              "\n 2. Post management:" +
                              "\n 3. Comment management:");
            
            int choice = Convert.ToInt32(Console.ReadLine());

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
    }
}
