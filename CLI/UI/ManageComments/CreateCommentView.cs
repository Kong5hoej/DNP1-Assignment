using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository)
{
    public async Task<Comment> AddCommentAsync(int postId, int userId, string? body)
    {
        Console.WriteLine("Creating comment...");
        Comment comment = new Comment(postId, userId, body);
        return await commentRepository.AddCommentAsync(comment) ??  throw new Exception($"Post creation failed, try again!");
    }
}