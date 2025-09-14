using Entities;
using RepositoryContracts;


namespace CLI.UI.ManagePosts;

public class ListPostsView(IPostRepository postRepository)
{
    private readonly IPostRepository postRepository = postRepository;

    public async Task<Post> GetOnePostAsync(int id)
    {
        return await postRepository.GetSinglePostAsync(id);
    }
    
    public IQueryable<Post> GetAllPosts()
    {
        return postRepository.GetManyPosts();
    }
    
    public async Task<Post> FindPostAsync(string? title, string? body)
    {
        int id = -1;
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
    
    public async Task<Post> FindPostAsync(int postId)
    {
        int id = -1;
        for (int i = 0; i < postRepository.GetManyPosts().Count(); i++)
        {
            if (postId == postRepository.GetManyPosts().ElementAt(i).Id)
                    id = postRepository.GetManyPosts().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await GetOnePostAsync(id);
    }
}