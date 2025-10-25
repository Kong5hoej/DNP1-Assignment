using ApiContracts.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDto> AddPost(CreatePostDto request);
    public Task<PostDto> UpdatePost(int id, UpdatePostDto request);
    public Task<GetSinglePostDto?> GetSinglePost(int id);
    public Task<List<PostDto>> GetManyPosts(string? title = null, int? userId = null, string? username = null);
    public Task<PostDto> DeletePost(int id);
}