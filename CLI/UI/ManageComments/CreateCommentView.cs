using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository)
{
    public async Task<Comment> AddCommentAsync(User user)
    {
        Console.WriteLine("Which postId do you want to comment?");
        int postId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("What do you want to write in the comment?");
        String? body = Console.ReadLine();
        Comment comment = new Comment(postId, user.Id, body);
        return await commentRepository.AddCommentAsync(comment) ??  throw new Exception($"Comment creation failed, try again!");
    }
    
    public async Task UpdateCommentAsync()
    {
        Console.WriteLine("Which comment do you want to update? (Write the ID of the comment)");
        int commentId = Convert.ToInt32(Console.ReadLine());
        Comment comment = await FindCommentAsync(commentId);
        await commentRepository.UpdateCommentAsync(comment);
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