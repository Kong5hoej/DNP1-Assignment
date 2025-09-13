using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;

    public async Task<Comment> GetOneCommentAsync(int id)
    {
        return await commentRepository.GetSingleCommentAsync(id);
    }
    
    public IQueryable<Comment> GetAllComments()
    {
        Console.WriteLine("Searching...");
        return commentRepository.GetManyComments();
    }
    
    public async Task<Comment> FindCommentAsync(int postId, string? body)
    {
        int id = -1;
        Console.WriteLine("Finding comment...");
        for (int i = 0; i < commentRepository.GetManyComments().Count(); i++)
        {
            if (postId == commentRepository.GetManyComments().ElementAt(i).PostId)
                if  (body == commentRepository.GetManyComments().ElementAt(i).Body)
                    id = commentRepository.GetManyComments().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await GetOneCommentAsync(id);
    }
}