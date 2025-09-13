namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public int Id { get; set; }
    
    public int PostId { get; set; }
    public int UserId { get; set; }
    
    public Comment(int postId, int userId, string? body)
    {
        PostId = postId;
        UserId = userId;
        Body = body;
    }
}