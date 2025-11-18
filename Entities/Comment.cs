namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public int Id { get; set; }
    
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    private Comment() {}
    public Comment(int postId, int userId, string? body)
    {
        PostId = postId;
        UserId = userId;
        Body = body;
    }
}