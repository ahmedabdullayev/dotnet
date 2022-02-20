using App.Domain;
using App.Domain.Identity;
using App.Domain.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    //For quiz feature
    public DbSet<Subject> Subjects { get; set; } = default!;

    public DbSet<Quiz> Quizzes { get; set; } = default!;

    public DbSet<Question> Questions { get; set; } = default!;

    public DbSet<Answer> Answers { get; set; } = default!;

    public DbSet<UserChoice> UserChoices { get; set; } = default!;

    public DbSet<UserQuiz> UserQuizzes { get; set; } = default!;
    
    // For Posts feature

    public DbSet<Topic> Topics { get; set; } = default!;

    public DbSet<UserPost> UserPosts { get; set; } = default!;

    public DbSet<UserComment> UserComments { get; set; } = default!;
    
    // For todos

    public DbSet<Todo> Todos { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}