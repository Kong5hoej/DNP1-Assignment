using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
{
    private readonly IPostRepository postRepository = postRepository;
    private readonly CreatePostView createPostView = new CreatePostView(postRepository);
    private readonly ListPostsView listPostsView = new ListPostsView(postRepository, commentRepository);

    public async Task StartAsync(User user)
    { 
        Console.WriteLine("What do you want to manage?" +
                          "\n 1. Create a new post" +
                          "\n 2. Update a post" +
                          "\n 3. List one post" +
                          "\n 4. List all posts" +
                          "\n 5. Delete an post");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                await createPostView.AddPostAsync(user);
                break;
            case 2:
                await createPostView.UpdatePostAsync();
                break;
            case 3:
                await listPostsView.GetOnePostAsync();
                break;
            case 4:
                listPostsView.GetAllPosts();
                break;
            case 5:
                await listPostsView.DeletePostAsync(user);
                break;
        }
    }
}