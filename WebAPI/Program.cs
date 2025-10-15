using Entities;
using FileRepositories;
using RepositoryContracts;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddScoped<IUserRepository, UserFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    var postRepo = scope.ServiceProvider.GetRequiredService<IPostRepository>();
    var commentRepo = scope.ServiceProvider.GetRequiredService<ICommentRepository>();

    var users = userRepo.GetManyUsers().ToList();
    if (!users.Any())
    {
        await userRepo.AddUserAsync(new User("Bob", "password"));
        await userRepo.AddUserAsync(new User("Michael", "password"));
        await userRepo.AddUserAsync(new User("Jan", "password"));
        await userRepo.AddUserAsync(new User("Erland", "password"));
        await userRepo.AddUserAsync(new User("Lars", "password"));
    }
    
    var posts = postRepo.GetManyPosts().ToList();
    if (!posts.Any())
    {
        await postRepo.AddPostAsync(new Post("I can program in Java", "If you need my help, I can program in Java!", 3));
        await postRepo.AddPostAsync(new Post("I can program in C#", "If you need my help, I can program in C#!", 2));
        await postRepo.AddPostAsync(new Post("I can program in HTML", "If you need my help, I can program in HTML!", 2));
        await postRepo.AddPostAsync(new Post("I can use an Arduino", "If you need my help, I use an Arduino!", 4));
        await postRepo.AddPostAsync(new Post("I can calculate big-O", "If you need my help, I can calculate big-0!", 5));
    }
    
    var comments = commentRepo.GetManyComments().ToList();
    if (!comments.Any())
    {
        await commentRepo.AddCommentAsync(new Comment(1, 2, "Please help me, Jan!"));
        await commentRepo.AddCommentAsync(new Comment(4, 1, "Help me use an Arduino!"));
        await commentRepo.AddCommentAsync(new Comment(3, 3, "Help me program in HTML, Michael!"));
        await commentRepo.AddCommentAsync(new Comment(5, 4, "I don't understand big-O, help me!"));
        await commentRepo.AddCommentAsync(new Comment(2, 5, "Let me analyse your C# program!"));
    }
}

app.UseHttpsRedirection();

app.Run();