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
        return commentRepository.GetManyComments();
    }
    
    public async Task<Comment> FindCommentAsync(int postId, string? body)
    {
        int id = -1;
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
    
    public async Task<Comment> FindCommentAsync(int postId, int commentId)
    {
        int id = -1;
        for (int i = 0; i < commentRepository.GetManyComments().Count(); i++)
        {
            if (postId == commentRepository.GetManyComments().ElementAt(i).PostId)
                if (commentId == commentRepository.GetManyComments().ElementAt(i).Id)
                    id = commentRepository.GetManyComments().ElementAt(i).Id;
        }

        if (id == -1)
        {
            return null;
        }
        return await GetOneCommentAsync(id);
    }
}