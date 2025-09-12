using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;




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
                              "\n 10. Delete an post");
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
                case 8: //List one post
                    Console.WriteLine("What is the title of your post?");
                    String? titleOne = Console.ReadLine();
                    Console.WriteLine("Write the body of your post");
                    String? bodyOne = Console.ReadLine();
                    Post post8 = await managePostsView.FindPost(titleOne, bodyOne);
                    Console.WriteLine(await managePostsView.GetOnePostAsync(post8.Id));
                    break;
                case 9: //List all posts
                    foreach (Post post9 in managePostsView.GetAllPosts())
                    {
                        Console.WriteLine($"{post9.Id}: {post9.Title} " +
                                          $"\n {post9.Body} \n");
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
                        Console.WriteLine("Try again!");
                    }
                    break;
            }
        }
    }
}
