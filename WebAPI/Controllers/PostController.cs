using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ApiContracts.Comments;
using ApiContracts.Posts;
using Entities;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly IUserRepository userRepo;
    private readonly ICommentRepository commentRepo;

    public PostController(IPostRepository postRepo, IUserRepository userRepo, ICommentRepository commentRepo)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
        this.commentRepo = commentRepo;
    }
    
    //Create
    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    { 
        try
        { 
            Post post = new(request.Title, request.Body, request.UserId);
            Post created = await postRepo.AddPostAsync(post);
            PostDto dto = new()
            {
                Id = created.Id,
                Title = created.Title,
                UserId =  created.UserId,
                Body = created.Body
            };
            return Created($"/Post/{dto.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int id, [FromBody] UpdatePostDto request)
    { 
        try
        {
            Post post = await postRepo.GetSinglePostAsync(id);
            post.Title = request.Title;
            post.Body = request.Body;

            await postRepo.UpdatePostAsync(post);

            PostDto dto = new()
            {
                Title =  post.Title,
                Body = post.Body,
                UserId = post.UserId,
                Id = post.Id
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
    public async Task<ActionResult<GetSinglePostDto>> GetSinglePost(int id)
    { 
        try
        { 
            Post post = await postRepo.GetSinglePostAsync(id);
            
            var allComments = commentRepo.GetManyComments().ToList();
            var relatedComments = allComments
                .Where(c => c.PostId == id)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Body = c.Body,
                    UserId = c.UserId,
                    PostId = c.PostId
                })
                .ToList();
            
            GetSinglePostDto dto = new()
            {
                Title =  post.Title,
                Body = post.Body,
                UserId = post.UserId,
                Comments =  relatedComments
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
    public async Task<ActionResult<IEnumerable<PostDto>>> GetManyPosts(
        [FromQuery] string? title = null,
        [FromQuery] int? userId = null,
        [FromQuery] string? username = null
    )
    { 
        try
        { 
            List<Post> posts = postRepo.GetManyPosts().ToList();
            
            if (!string.IsNullOrWhiteSpace(title))
                posts = posts
                    .Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (userId.HasValue)
                posts = posts
                    .Where(p => p.UserId == userId.Value)
                    .ToList();

            if (!string.IsNullOrWhiteSpace(username))
            {
                var users = userRepo.GetManyUsers().ToList();
                var user = users.FirstOrDefault(u => 
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                    posts = posts.Where(p => p.UserId == user.Id).ToList();
                else
                    posts.Clear(); // ingen brugere matcher -> tomt resultat
            }
            
            var dtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId
            }).ToList();
            
            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occurred while retrieving posts.");
        }
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<PostDto>> DeletePost(int id)
    { 
        try
        { 
            await postRepo.DeletePostAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}