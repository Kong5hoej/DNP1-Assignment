﻿using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<Comment> GetSingleCommentAsync(int id);
    IQueryable<Comment> GetManyComments();
    Task<List<Comment>> GetCommentsByPostId(int postId);
    void DummyData();
}