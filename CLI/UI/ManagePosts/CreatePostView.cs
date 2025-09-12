using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView (IPostRepository postRepository)
{
    private readonly IPostRepository postRepository = postRepository;

    public async Task<Post> AddPostAsync(string? title, string? body, int userId)
    {
        Console.WriteLine("Creating post...");
        Post post = new Post(title, body, userId);
        return await postRepository.AddPostAsync(post) ??  throw new Exception($"Post creation failed, try again!");
    }
}
