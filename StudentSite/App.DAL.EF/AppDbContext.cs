using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppUser> AppUsers { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

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
    
    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }


    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            builder
                .Entity<App.Domain.Answer>()
                .Property(e => e.AnswerText)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            
            builder
                .Entity<App.Domain.Question>()
                .Property(e => e.QuestionText)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            ///
            builder
                .Entity<App.Domain.Quiz>()
                .Property(e => e.Name)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<App.Domain.Quiz>()
                .Property(e => e.Description)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            ///
            builder
                .Entity<App.Domain.Subject>()
                .Property(e => e.Name)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<App.Domain.Subject>()
                .Property(e => e.Description)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            ///
            builder
                .Entity<App.Domain.Topic>()
                .Property(e => e.Name)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<App.Domain.Topic>()
                .Property(e => e.Description)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
        }
    }
    
    
    private static string SerialiseLangStr(LangStr lStr) => System.Text.Json.JsonSerializer.Serialize(lStr);

    private static LangStr DeserializeLangStr(string jsonStr) =>
        System.Text.Json.JsonSerializer.Deserialize<LangStr>(jsonStr) ?? new LangStr();
}