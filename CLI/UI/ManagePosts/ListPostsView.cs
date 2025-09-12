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
    
    public async Task<Post> FindPostAsync(string? title, string? body)
    {
        int id = -1;
        Console.WriteLine("Finding post...");
        for (int i = 0; i < postRepository.GetManyPosts().Count(); i++)
        {
            if (title == postRepository.GetManyPosts().ElementAt(i).Title)
                if  (body == postRepository.GetManyPosts().ElementAt(i).Body)
                    id = postRepository.GetManyPosts().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await GetOnePostAsync(id);
    }
}