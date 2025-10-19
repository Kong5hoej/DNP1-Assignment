using ApiContracts.Comments;

namespace ApiContracts.Posts;

public class GetSinglePostDto
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public List<CommentDto> Comments { get; set; } = new();
}