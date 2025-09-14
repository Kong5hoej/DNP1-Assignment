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
        await postRepository.UpdatePostAsync(post);
    }

    public async Task DeletePostAsync(Post post, int userId)
    {
        if (post.UserId != userId)
            throw new Exception("You can only delete your own posts!");
        await postRepository.DeletePostAsync(post.Id);
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
    
    public async Task<Post> FindPost(string? title, string? body)
    {
        return await listPostsView.FindPostAsync(title, body);
    }
    
    public async Task<Post> FindPost(int postId)
    {
        return await listPostsView.FindPostAsync(postId);
    }
}