using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppUser> AppUsers { get; set; } = default!;
    
    public DbSet<Subject> Subjects { get; set; } = default!;

    public DbSet<Semester> Semesters { get; set; } = default!;

    public DbSet<Enrollment> Enrollments { get; set; } = default!;

    public DbSet<Homework> Homeworks { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}