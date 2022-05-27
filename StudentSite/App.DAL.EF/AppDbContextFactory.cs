using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.DAL.EF;


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Server=ahabduhayusserver.postgres.database.azure.com;Database=ahabduhayusserver;Port=5432;User Id=ahabduadmin007@ahabduhayusserver;Password=geVfz2u73yES7Zd;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
