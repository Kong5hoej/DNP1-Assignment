using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new List<Comment>();
    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }
    
    public Task UpdateCommentAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }
    
    public Task DeleteCommentAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }
    
    public Task<Comment> GetSingleCommentAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        return Task.FromResult(comment);
    }
    
    public IQueryable<Comment> GetManyComments()
    {
        return comments.AsQueryable();
    }
}