using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView (IPostRepository postRepository)
{
    private readonly IPostRepository postRepository = postRepository;

    public async Task<Post> AddPostAsync(User user)
    {
        Console.WriteLine("What is the title of your post?");
        String? title = Console.ReadLine();
        Console.WriteLine("What is the body of your post?");
        String? body = Console.ReadLine();
        
        Post post = new Post(title, body, user.Id);
        return await postRepository.AddPostAsync(post) ??  throw new Exception($"Post creation failed, try again!");
    }
    public async Task UpdatePostAsync()
    {
        Console.WriteLine("Which post do you want to update? (Write the title of the post)");
        String? postTitle = Console.ReadLine();
        
        //await postRepository.UpdatePostAsync(postTitle);
    }
}
