﻿using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddPostAsync(Post post);
    Task UpdatePostAsync(String? postTitle);
    Task DeletePostAsync(int id);
    Task<Post> GetSinglePostAsync(int id);
    IQueryable<Post> GetManyPosts();
}