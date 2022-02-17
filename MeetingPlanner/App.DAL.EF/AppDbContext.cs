using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Meeting> Meetings { get; set; } = default!;
    public DbSet<MeetingOption> MeetingOptions { get; set; } = default!;
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}