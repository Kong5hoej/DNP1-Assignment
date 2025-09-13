using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

/*
 * Få lavet:
 * Implementeret klasser og CLI for comments
 * Tjek CLI for posts
 *
 * Iflg requirements:
 * View specific post with title, body and comments (mangler bare comments, se nr. 8)
 *
 * Tankeboks:
 * Lav login/logout-funktionalitet, så man ikke skal angive username og password hele tiden
 * Lav nogle metodekald i nogle metoder, så det reducerer arbejde i CliApp
 * Skal man kunne liste alle comments fra en speciel bruger?
 */


public class CliApp( 
    IUserRepository userRepository,
    ICommentRepository commentRepository,
    IPostRepository postRepository)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly ICommentRepository commentRepository = commentRepository;
    private readonly IPostRepository postRepository = postRepository;
    
    private ManageUsersView manageUsersView = new ManageUsersView(userRepository);
    private ManagePostsView managePostsView = new ManagePostsView(postRepository);
    private ManageCommentsView manageCommentsView = new ManageCommentsView(commentRepository);

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\n What do you want to do? " +
                              "\n User management:" +
                              "\n 1. Create a new user " +
                              "\n 2. Update an user" +
                              "\n 3. List one user " +
                              "\n 4. List all users " +
                              "\n 5. Delete an user" +
                              "\n Post management:" +
                              "\n 6. Create a new post" +
                              "\n 7. Update a post" +
                              "\n 8. List one post" +
                              "\n 9. List all posts" +
                              "\n 10. Delete an post" +
                              "\n Comment management:" +
                              "\n 11. Create a comment" +
                              "\n 12. Update a comment" +
                              "\n 13. Delete a comment");
            
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("What will the username be?");
                    String? username1 = Console.ReadLine();
                    Console.WriteLine("What will the password be?");
                    String? password1 = Console.ReadLine();
                    try
                    {
                        await manageUsersView.AddUserAsync(username1, password1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Username is already in use. Enter a different username: ");
                        username1 = Console.ReadLine();
                        await manageUsersView.AddUserAsync(username1, password1);
                    }
                    Console.WriteLine("The user has now been created!");
                    break;
                case 2:
                    Console.WriteLine("What is your username?");
                    String? username2 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password2 = Console.ReadLine();
                    User user2 = await manageUsersView.FindUser(username2, password2);
                    await manageUsersView.UpdateUserAsync(user2);
                    Console.WriteLine("The user has now been updated!");
                    break;
                case 3:
                    Console.WriteLine("What is your username?");
                    String? username3 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password3 = Console.ReadLine();
                    User user3 = await manageUsersView.FindUser(username3, password3);
                    await manageUsersView.GetOneUserAsync(user3.Id);
                    Console.WriteLine($"The user has now been retrieved! Your username is {user3.Username} and your ID is {user3.Id}");
                    break;
                case 4:
                    foreach (User user4 in manageUsersView.GetAllUsers())
                    {
                        Console.WriteLine($"{user4.Id}: {user4.Username}");
                    }
                    break;
                case 5:
                    Console.WriteLine("What is your username?");
                    String? username5 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password5 = Console.ReadLine();
                    User user5 = await manageUsersView.FindUser(username5, password5);
                    await manageUsersView.DeleteUserAsync(user5);
                    break;
                case 6: //Create post
                    Console.WriteLine("What is your username?");
                    String? username6 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password6 = Console.ReadLine();
                    if (await manageUsersView.FindUser(username6, password6) == null)
                    {
                        Console.WriteLine("The user is not in our system!");
                        break;
                    }
                    User user6 = await manageUsersView.FindUser(username6, password6);
                    Console.WriteLine("What is the title of your post?");
                    String? titleCreate = Console.ReadLine();
                    Console.WriteLine("Write the body of your post");
                    String? bodyCreate = Console.ReadLine();
                    await managePostsView.AddPostAsync(titleCreate, bodyCreate, user6.Id);
                    Console.WriteLine("The post has been created!");
                    break;
                case 7: //Update post
                    Console.WriteLine("What is the title of your post?");
                    String? titleUpdate = Console.ReadLine();
                    Console.WriteLine("Write the body of your post");
                    String? bodyUpdate = Console.ReadLine();
                    Post post7 = await managePostsView.FindPost(titleUpdate, bodyUpdate);
                    await managePostsView.UpdatePostAsync(post7);
                    Console.WriteLine("The post has now been updated!");
                    break;
                case 8: //List one post - Should be with comments!!!
                    Console.WriteLine("What is the ID of the post?");
                    int postId = Convert.ToInt32(Console.ReadLine());
                    Post post8 = await managePostsView.GetOnePostAsync(postId);
                    Console.WriteLine($"{post8.Id}: {post8.Title} " +
                                      $"\n {post8.Body} \n" +
                                      $"\n Comments:");
                    foreach (Comment comment8 in manageCommentsView.GetAllComments())
                    {
                        if (comment8.PostId == postId)
                        {
                            Console.WriteLine($"{comment8.Id}: {comment8.Body}");
                        }
                            
                    }
                    
                    
                    break;
                case 9: //List all posts
                    foreach (Post post9 in managePostsView.GetAllPosts())
                    {
                        Console.WriteLine($"{post9.Id}: {post9.Title}");
                    }
                    break;
                case 10: //Delete a post
                    Console.WriteLine("What is your username?");
                    String? username10 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password10 = Console.ReadLine();
                    User user10 = await manageUsersView.FindUser(username10, password10);
                    Console.WriteLine("What is the title of your post?");
                    String? titleDel = Console.ReadLine();
                    Console.WriteLine("Write the body of your post");
                    String? bodyDel = Console.ReadLine();
                    Post post10 = await managePostsView.FindPost(titleDel, bodyDel);
                    try
                    {
                        await managePostsView.DeletePostAsync(post10, user10.Id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 11: //Create a comment
                    Console.WriteLine("What is your username?");
                    String? username11 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password11 = Console.ReadLine();
                    if (await manageUsersView.FindUser(username11, password11) == null)
                    {
                        Console.WriteLine("The user is not in our system!");
                        break;
                    }
                    User user11 = await manageUsersView.FindUser(username11, password11);
                    Console.WriteLine("What is the ID of the post?"); 
                    int postId11 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write the body of your comment?");
                    String? comment11 = Console.ReadLine();
                    await manageCommentsView.AddCommentAsync(postId11, user11.Id, comment11);
                    Console.WriteLine("The post has been created!");
                    break;
                case 12: //Update a comment
                    Console.WriteLine("What is the ID of the post?");
                    int postId12 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write the body of your comment?");
                    String? commentBody12 = Console.ReadLine();
                    Comment comment12 = await manageCommentsView.FindComment(postId12, commentBody12);
                    await manageCommentsView.UpdateCommentAsync(comment12);
                    Console.WriteLine("The comment has been updated!");
                    break;
                case 13: //Delete a comment
                    Console.WriteLine("What is your username?");
                    String? username13 = Console.ReadLine();
                    Console.WriteLine("What is your password?");
                    String? password13 = Console.ReadLine();
                    User user13 = await manageUsersView.FindUser(username13, password13);
                    Console.WriteLine("What is the ID of your post?");
                    int postId13 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write the body of your comment");
                    String? commentBody13 = Console.ReadLine();
                    Comment comment13 = await manageCommentsView.FindComment(postId13, commentBody13);
                    try
                    {
                        await manageCommentsView.DeleteCommentAsync(comment13, user13.Id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
            }
        }
    }
}
