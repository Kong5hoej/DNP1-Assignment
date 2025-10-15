namespace ApiContracts.Comments;

public class GetSingleCommentDto
{
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}