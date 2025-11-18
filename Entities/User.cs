namespace Entities;

public class User
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Id { get; set; }
    
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Post> Posts { get; set; } = new List<Post>();
    private User() {}
    public User(string? username, string? password)
    {
        Username = username;
        Password = password;
    }
}