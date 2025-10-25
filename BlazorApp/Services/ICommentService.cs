using ApiContracts.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CommentDto> AddComment(CreateCommentDto request);
    public Task<CommentDto> UpdateComment(int postId, UpdateCommentDto request);
    public Task<GetSingleCommentDto?> GetSingleComment(int id);
    public Task<List<CommentDto>> GetManyComments(int? userId = null, string? username = null, int? postId = null);
    public Task<CommentDto> DeleteComment(int id);
}