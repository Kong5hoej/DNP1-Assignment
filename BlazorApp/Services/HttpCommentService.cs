using System.Text.Json;
using ApiContracts.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CommentDto> AddComment(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"/Posts/{request.PostId}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Response status: {httpResponse.StatusCode}");
        Console.WriteLine($"Response body: {response}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            //throw new Exception(response);
            throw new Exception($"Error adding comment: {response}");
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<GetSingleCommentDto?> GetSingleComment(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<GetSingleCommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<List<CommentDto>> GetManyComments(int? userId = null, string? username = null, int? postId = null)
    {
        var queryParams = new List<string>();

        if (userId.HasValue)
            queryParams.Add($"userId={userId.Value}");
        if (!string.IsNullOrEmpty(username))
            queryParams.Add($"username={Uri.EscapeDataString(username)}");
        if (postId.HasValue)
            queryParams.Add($"postId={postId.Value}");

        var query = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;

        HttpResponseMessage httpResponse = await client.GetAsync($"comments{query}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception($"Kunne ikke hente kommentarer: {response}");

        return JsonSerializer.Deserialize<List<CommentDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<CommentDto>();
    }

    public async Task<CommentDto> UpdateComment(int id, UpdateCommentDto request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"/Posts/{request.Id}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<CommentDto> DeleteComment(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"comments/delete/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}