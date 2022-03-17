dotnet ef migrations --project App.DAL.EF --startup-project WebApp add Initial
dotnet ef database --project App.DAL.EF --startup-project WebApp update
dotnet ef database --project App.DAL.EF --startup-project WebApp drop

dotnet aspnet-codegenerator controller -name SubjectsController        -actions -m  Subject        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizzesController        -actions -m  Quiz        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuestionsController        -actions -m  Question        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AnswersController        -actions -m  Answer        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserChoicesController        -actions -m  UserChoice        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserQuizzesController        -actions -m  UserQuiz        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TopicsController        -actions -m  Topic        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserPostsController        -actions -m  UserPost        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserCommentsController        -actions -m  UserComment        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TodosController        -actions -m  Todo        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

Api Controllers

~~~
dotnet aspnet-codegenerator controller -name SubjectsController     -m App.Domain.Subject     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~


// ADMIN AREA

dotnet aspnet-codegenerator controller -name SubjectsController        -actions -m  Subject        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizzesController        -actions -m  Quiz        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuestionsController        -actions -m  Question        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AnswersController        -actions -m  Answer        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserChoicesController        -actions -m  UserChoice        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserQuizzesController        -actions -m  UserQuiz        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
