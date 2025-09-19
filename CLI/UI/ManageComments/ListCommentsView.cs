using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;

    public async Task DeleteCommentAsync(User user)
    {
        Console.WriteLine("What is the ID of the comment?");
        int commentId = Convert.ToInt32(Console.ReadLine());
        Comment comment = await FindCommentAsync(commentId);
        
        if (comment.UserId != user.Id)
            throw new Exception("You can only delete your own comments!");
        await commentRepository.DeleteCommentAsync(comment.Id);
    }
    
    public async Task<Comment> GetOneCommentAsync(int id)
    {
        return await commentRepository.GetSingleCommentAsync(id);
    }
    
    public IQueryable<Comment> GetAllComments()
    {
        return commentRepository.GetManyComments();
    }
    
    public async Task<Comment> FindCommentAsync(int commentId)
    {
        int id = -1;
        for (int i = 0; i < commentRepository.GetManyComments().Count(); i++)
        {
            if (commentId == commentRepository.GetManyComments().ElementAt(i).Id)
                id = commentRepository.GetManyComments().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await commentRepository.GetSingleCommentAsync(id);
    }
}