using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView (ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly CreateCommentView createCommentView = new CreateCommentView(commentRepository);
    private readonly ListCommentsView listCommentsView = new ListCommentsView(commentRepository);

    public async Task StartAsync(User user)
    {
        Console.WriteLine("What do you want to manage?" +
                          "\n 1. Create a new comment" +
                          "\n 2. Update a comment" +
                          "\n 3. Delete a comment" +
                          "\n 4. Exit");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 4)
        {
            return;
        }

        switch (choice)
        {
            case 1:
                await createCommentView.AddCommentAsync(user);
                break;
            case 2:
                await createCommentView.UpdateCommentAsync();
                break;
            case 3:
                await listCommentsView.DeleteCommentAsync(user);
                break;
        }
    }
}