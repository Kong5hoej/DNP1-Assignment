using Entities;
using RepositoryContracts;


namespace CLI.UI.ManagePosts;

public class ListPostsView(IPostRepository postRepository)
{
    private readonly IPostRepository postRepository = postRepository;

    public async Task<Post> GetOnePostAsync(int id)
    {
        Console.WriteLine("Searching for a single post...");
        return await postRepository.GetSinglePostAsync(id);
    }
    
    public IQueryable<Post> GetAllPosts()
    {
        Console.WriteLine("Searching...");
        return postRepository.GetManyPosts();
    }
}