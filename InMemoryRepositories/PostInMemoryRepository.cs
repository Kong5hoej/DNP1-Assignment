using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts =  new List<Post>();
    public Task<Post> AddPostAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }
    
    public Task UpdatePostAsync(String? postTitle)
    {
        Post post = null;
        foreach (Post p in posts)
        {
            if (p.Title == postTitle) 
                post = p;
        }

        if (post == null)
        {
            throw new Exception("Post not found");
        }
        
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }
    
    public Task DeletePostAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Post> GetSinglePostAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        
        return Task.FromResult(post);
    }
    
    public IQueryable<Post> GetManyPosts()
    {
        return posts.AsQueryable();
    }

    public void DummyData()
    {
        AddPostAsync(new Post("I can program in Java", "If you need my help, I can program in Java!", 3));
        AddPostAsync(new Post("I can program in C#", "If you need my help, I can program in C#!", 2));
        AddPostAsync(new Post("I can program in HTML", "If you need my help, I can program in HTML!", 2));
        AddPostAsync(new Post("I can use an Arduino", "If you need my help, I use an Arduino!", 4));
        AddPostAsync(new Post("I can calculate big-O", "If you need my help, I can calculate big-0!", 5));
    }
}