using App.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.
            ApplicationServices.
            GetRequiredService<IServiceScopeFactory>().
            CreateScope();

        using var context = serviceScope
            .ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }
        
        // TODO - Check database state
        // can't connect - wrong address
        // can't connect - wrong user/pass
        // can connect - but no database
        // can connect - there is database

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            // TODO
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            // TODO
            // var f = new FooBar
            // {
            //     Name =
            //     {
            //         ["en"] = "English",
            //         ["et"] = "Eesti",
            //         ["ru"] = "Русский",
            //     }
            // };
            // context.FooBars.Add(f);
            // context.SaveChanges();
        }
    }

}