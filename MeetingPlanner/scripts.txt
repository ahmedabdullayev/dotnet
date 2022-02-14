dotnet ef migrations --project DAL.App.EF --startup-project WebApp add Initial
dotnet ef database --project DAL.App.EF --startup-project WebApp update
dotnet ef database --project DAL.App.EF --startup-project WebApp drop

            dotnet ef migrations add InitialMigration --project BookStoreApi
            dotnet ef database update --project BookStoreApi
            dotnet ef database drop  --project BookStoreApi
            
MVC Web controllers
~~~
dotnet aspnet-codegenerator controller -name PersonsController        -actions -m  Person        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactsController       -actions -m  Contact       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactTypesController   -actions -m  ContactType   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SimplesController   -actions -m  Simple   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

dotnet aspnet-codegenerator controller -name RolesController        -actions -m  AppRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f




Api controllers
~~~
dotnet aspnet-codegenerator controller -name PersonsController     -m Person     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactsController     -m Contact     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactTypesController     -m ContactType     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SimplesController     -m Simple     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

Scaffold the identity page
~~~
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f


 //disable cascade delete initially for everything
    foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
    {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
