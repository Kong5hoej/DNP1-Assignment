using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView(IPostRepository postRepository)
{
    private readonly IPostRepository postRepository = postRepository;
    private readonly CreatePostView createPostView = new CreatePostView(postRepository);
    private readonly ListPostsView listPostsView = new ListPostsView(postRepository);
    
    public async Task UpdatePostAsync(Post post)
    { 
        Console.WriteLine("Updating post...");
        await postRepository.UpdatePostAsync(post);
        Console.WriteLine("Post updated");
    }

    public async Task DeletePostAsync(Post post)
    {
        Console.WriteLine("Deleting Post...");
        await postRepository.DeletePostAsync(post.Id);
        Console.WriteLine("Post deleted");
    }

    public async Task<Post> AddPostAsync(string? title, string? body, int userId)
    {
        return await createPostView.AddPostAsync(title, body, userId);
    }

    public async Task<Post> GetOnePostAsync(int id)
    {
        return await listPostsView.GetOnePostAsync(id);
    }

    public IQueryable<Post> GetAllPosts()
    {
        return listPostsView.GetAllPosts();
    }
}