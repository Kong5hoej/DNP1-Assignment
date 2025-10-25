using ApiContracts.Comments;
using ApiContracts.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]s")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository commentRepo;
    private readonly IUserRepository userRepo;

    public CommentController(ICommentRepository commentRepo, IUserRepository userRepo)
    {
        this.commentRepo = commentRepo;
        this.userRepo = userRepo;
    }
    
    //Create
    [HttpPost("/Posts/{postId:int}")]
    public async Task<ActionResult<CommentDto>> AddComment(int postId, [FromBody] CreateCommentDto request)
    { 
        try
        { 
            Comment comment = new(postId, request.UserId, request.Body);
            Comment created = await commentRepo.AddCommentAsync(comment);
            CommentDto dto = new()
            {
                Id = created.Id,
                UserId =  created.UserId,
                Body = created.Body,
                PostId = postId
            };
            return Created($"/Posts/{dto.PostId}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Update
    [HttpPut("/Posts/{postId:int}")]
    public async Task<ActionResult<CommentDto>> UpdateComment(int postId, [FromBody] UpdateCommentDto request)
    { 
        try
        {
            Comment comment = await commentRepo.GetSingleCommentAsync(request.Id);
            
            comment.Body = request.Body;

            await commentRepo.UpdateCommentAsync(comment);

            CommentDto dto = new()
            {
                Body = comment.Body,
                UserId = comment.UserId,
                Id = comment.Id,
                PostId = postId
            };
            
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Get single
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetSingleCommentDto>> GetSingleComment(int id)
    { 
        try
        { 
            Comment comment = await commentRepo.GetSingleCommentAsync(id);
            GetSingleCommentDto dto = new()
            {
                Body = comment.Body,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //GetMany
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetManyComments(
        [FromQuery] int? userId = null,
        [FromQuery] string? username = null,
        [FromQuery] int? postId = null)
    { 
        try
        { 
            List<Comment> comments = commentRepo.GetManyComments().ToList();

            if (userId.HasValue)
                comments = comments
                    .Where(c => c.UserId == userId.Value)
                    .ToList();

            if (!string.IsNullOrWhiteSpace(username))
            {
                var users = userRepo.GetManyUsers().ToList();
                var user = users.FirstOrDefault(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                    comments = comments
                        .Where(c => c.UserId == user.Id)
                        .ToList();
                else
                    comments.Clear();
            }

            if (postId.HasValue)
                comments = comments
                    .Where(c => c.PostId == postId.Value)
                    .ToList();

            var dtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                UserId = c.UserId,
                PostId = c.PostId
            }).ToList();

            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<CommentDto>> DeleteComment(int id)
    { 
        try
        { 
            await commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}