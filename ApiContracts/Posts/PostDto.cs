﻿namespace ApiContracts.Posts;

public class PostDto
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public int Id { get; set; }
}