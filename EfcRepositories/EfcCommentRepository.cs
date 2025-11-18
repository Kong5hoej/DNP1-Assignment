using Entities;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }


    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        List<Comment> comments = ctx.Comments.AsQueryable().ToList();
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        ctx.Comments.Add(comment);
        await ctx.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        List<Comment> comments = ctx.Comments.AsQueryable().ToList();
        
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        Comment? commentToRemove = ctx.Comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        ctx.Comments.Remove(commentToRemove);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleCommentAsync(int id)
    {
        Comment? comment = ctx.Comments.SingleOrDefault(c => c.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetManyComments()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task<List<Comment>> GetCommentsByPostId(int postId)
    {
        List<Comment> comments = ctx.Comments.AsQueryable().ToList();
        return comments.Where(c => c.PostId == postId).ToList();
    }
}