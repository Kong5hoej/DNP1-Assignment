using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ApiContracts;
using ApiContracts.Posts;
using ApiContracts.Users;
using Entities;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
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
    public async Task<ActionResult<PostDto>> GetSinglePost(int id)
    { 
        try
        { 
            Post post = await postRepo.GetSinglePostAsync(id);
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
    
    //GetMany
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetManyPosts()
    { 
        try
        { 
            List<Post> posts = postRepo.GetManyPosts().ToList();
            
            var dtos = posts.Select(u => new PostDto
            {
                Title =  u.Title,
                Body = u.Body,
                UserId = u.UserId,
                Id = u.Id
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