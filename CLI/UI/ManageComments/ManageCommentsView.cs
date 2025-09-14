using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView (ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly CreateCommentView createCommentView = new CreateCommentView(commentRepository);
    private readonly ListCommentsView listCommentsView = new ListCommentsView(commentRepository);
    
    public async Task UpdateCommentAsync(Comment comment)
    { 
        await commentRepository.UpdateCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(Comment comment, int userId)
    {
        if (comment.UserId != userId)
            throw new Exception("You can only delete your own comments!");
        await commentRepository.DeleteCommentAsync(comment.Id);
    }

    public async Task<Comment> AddCommentAsync(int postId, int userId, string? body)
    {
        return await createCommentView.AddCommentAsync(postId, userId, body);
    }

    public async Task<Comment> GetOneCommentAsync(int id)
    {
        return await listCommentsView.GetOneCommentAsync(id);
    }

    public IQueryable<Comment> GetAllComments()
    {
        return listCommentsView.GetAllComments();
    }
    
    public async Task<Comment> FindComment(int postId, string? body)
    {
        return await listCommentsView.FindCommentAsync(postId, body);
    }
    
    public async Task<Comment> FindComment(int postId, int commentId)
    {
        return await listCommentsView.FindCommentAsync(postId, commentId);
    }
}