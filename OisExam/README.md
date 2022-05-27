dotnet ef migrations --project App.DAL.EF --startup-project WebApp add Initial
dotnet ef database --project App.DAL.EF --startup-project WebApp update
dotnet ef database --project App.DAL.EF --startup-project WebApp drop

~~~
dotnet aspnet-codegenerator controller -name AnswersController     -m App.Domain.Answer     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuestionsController     -m App.Domain.Question     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~
