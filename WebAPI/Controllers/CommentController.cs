using ApiContracts.Comments;
using ApiContracts.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentController(ICommentRepository commentRepo, IPostRepository postRepo)
    {
        this.commentRepo = commentRepo;
    }
    
    //Create
    [HttpPost("/Post/{postId:int}")]
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
            return Created($"/Post/{dto.PostId}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Update
    [HttpPut("/Post/{postId:int}")]
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
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetManyComments()
    { 
        try
        { 
            List<Comment> comments = commentRepo.GetManyComments().ToList();
            
            var dtos = comments.Select(u => new CommentDto
            {
                Body = u.Body,
                UserId = u.UserId,
                Id = u.Id,
                PostId = u.PostId
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