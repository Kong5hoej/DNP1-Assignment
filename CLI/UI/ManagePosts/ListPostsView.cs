using Entities;
using RepositoryContracts;


namespace CLI.UI.ManagePosts;

public class ListPostsView(IPostRepository postRepository, ICommentRepository commentRepository)
{
    private readonly IPostRepository postRepository = postRepository;
    private readonly ICommentRepository commentRepository = commentRepository;

    public async Task<Post> GetOnePostAsync()
    {
        Console.WriteLine("What is the ID of the post, you would like?");
        int postId = Convert.ToInt32(Console.ReadLine());

        Post post = await postRepository.GetSinglePostAsync(postId);
        
        Console.WriteLine($"{postId}: {post.Title}" +
                          $"\n AuthorID: {post.UserId} " +
                          $"\n {post.Body} " +
                          $"\n Comments:");
        foreach (Comment comment in await commentRepository.GetCommentsByPostId(postId))
        {
            Console.WriteLine($"{comment.UserId}: {comment.Body}");
        }
        
        return await postRepository.GetSinglePostAsync(postId);
    }
    
    public IQueryable<Post> GetAllPosts()
    {
        if (!postRepository.GetManyPosts().Any())
        {
            Console.WriteLine("There are no posts in our system!");
        }
        foreach (Post post in postRepository.GetManyPosts())
        {
            Console.WriteLine($"{post.Id}: {post.Title}");
        }
        
        return postRepository.GetManyPosts();
    }
    
    public async Task DeletePostAsync(User user)
    {
        Console.WriteLine("What is the title of the post you would like to delete?");
        String? postTitle = Console.ReadLine();
        Post post = await FindPostAsync(postTitle);
        if (post.UserId != user.Id)
            throw new Exception("You can only delete your own posts!");
        await postRepository.DeletePostAsync(post.Id);
    }
    
    public async Task<Post> FindPostAsync(string? title)
    {
        int id = -1;
        for (int i = 0; i < postRepository.GetManyPosts().Count(); i++)
        {
            if (title == postRepository.GetManyPosts().ElementAt(i).Title)
                    id = postRepository.GetManyPosts().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await postRepository.GetSinglePostAsync(id);
    }
}