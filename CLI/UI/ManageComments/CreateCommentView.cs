using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository)
{
    public async Task<Comment> AddCommentAsync(int postId, int userId, string? body)
    {
        Comment comment = new Comment(postId, userId, body);
        return await commentRepository.AddCommentAsync(comment) ??  throw new Exception($"Comment creation failed, try again!");
    }
}