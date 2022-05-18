using System;
using System.Linq;
using System.Threading.Tasks;
using App.BLL;
using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Todo = App.BLL.DTO.Todo;

namespace Testing.WebApp;

public class TestTodoUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _ctx;
    private DbContextOptionsBuilder<AppDbContext> optionsBuilder;

    public TestTodoUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<ITodoService>();
        _ctx = new AppDbContext(optionsBuilder.Options);
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated(); 
    }

    public IAppBLL GetBLL()
    {
        var context = new AppDbContext(optionsBuilder.Options);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<App.DAL.EF.AutomapperConfig>();
            cfg.AddProfile<App.BLL.AutomapperConfig>();
        });
        var mapper = mockMapper.CreateMapper();
        var uow = new AppUOW(context, mapper);
        return new AppBLL(uow, mapper);
    }


    [Fact]
    public async Task Action_Test_CreateTodo()
    {
        var testBll = GetBLL();

        var userForTesting = await CreateUserForTesting("ahmed","abdu");
        _testOutputHelper.WriteLine(userForTesting.Id.ToString());
        
        Guid guid = Guid.NewGuid();
        var todo = testBll.Todos.Add(new Todo
        {
            Id = new Guid(guid.ToString()),
            TodoText = "HW3 Käver for next lecture",
            IsDone = false,
            AppUserId = userForTesting.Id,
        });
        await testBll.SaveChangesAsync();
        var existsAsync = await testBll.Todos.ExistsAsync(guid);
        // or check with that: var result = await _ctx.Todos.AnyAsync(e => e.Id == guid);
        Assert.True(existsAsync);
    }
    
    [Fact]
    public async Task Action_Test_OneTodo()
    {
        var testBll = GetBLL();

        var userForTesting = await CreateUserForTesting("ahmed","abdu");

        Guid guid = Guid.NewGuid();
        var todo = testBll.Todos.Add(new Todo
        {
            Id = new Guid(guid.ToString()),
            TodoText = "HW10",
            IsDone = false,
            AppUserId = userForTesting.Id,
        });
        await testBll.SaveChangesAsync();
        var firstOrDefault = await testBll.Todos.FirstOrDefaultAsync(guid);
        Assert.NotNull(firstOrDefault);
        Assert.Equal("HW10", firstOrDefault!.TodoText);
    
    }

    [Fact]
    public async Task Action_Test_DeleteTodo()
    {
        var testBll = GetBLL();
        
        var userForTesting = await CreateUserForTesting("peeter","abdu");
        
        Guid guid = Guid.NewGuid();
        var todo = testBll.Todos.Add(new Todo
        {
            Id = new Guid(guid.ToString()),
            TodoText = "HW3 Käver for next lecture",
            IsDone = false,
            AppUserId = userForTesting.Id,
        });
        await testBll.SaveChangesAsync();
        // To avoid tracing issues
        testBll = GetBLL();
        var created = await testBll.Todos.FirstWithUser(guid, userForTesting.Id);
        // _testOutputHelper.WriteLine(created!.TodoText);
        //TEST
        Assert.NotNull(created);
        Assert.Equal("HW3 Käver for next lecture", created!.TodoText);

        await testBll.Todos.RemoveAsync(created.Id);
        await testBll.SaveChangesAsync();
        var data = await GetBLL().Todos.GetAllAsync(userForTesting.Id);
        Assert.Empty(data);
    }

    [Fact]
    public async Task Action_Test_EditTodo()
    {
        var testBll = GetBLL();
        
        var userForTesting = await CreateUserForTesting("peeter","abdu");
        
        Guid guid = Guid.NewGuid();
        var todo = new Todo()
        {
            Id = new Guid(guid.ToString()),
            TodoText = "HW3 Käver for next lecture",
            IsDone = false,
            AppUserId = userForTesting.Id,
        };
        testBll.Todos.Add(todo);
        await testBll.SaveChangesAsync();
        // To avoid tracing issues
        testBll = GetBLL();
        var created = await testBll.Todos.FirstWithUser(guid, userForTesting.Id);
        // _testOutputHelper.WriteLine(created!.TodoText);
        //TEST
        Assert.NotNull(created);
        Assert.Equal("HW3 Käver for next lecture", created!.TodoText);
        todo.TodoText = "Math hw";
        testBll.Todos.UpdateWithUser(todo, userForTesting.Id);
        await testBll.SaveChangesAsync();
        var edited = await testBll.Todos.FirstWithUser(guid, userForTesting.Id);
        _testOutputHelper.WriteLine(edited!.TodoText);
        Assert.NotNull(edited);
        Assert.Equal("Math hw", edited!.TodoText);
    }
    
    [Fact]
    public async Task Action_Test_GetAll()
    {
        var testBll = GetBLL();

        var userForTesting = await CreateUserForTesting("peeter", "abdu");
        Guid guid1 = Guid.NewGuid();
        var todo1 = new Todo()
        {
            Id = new Guid(guid1.ToString()),
            TodoText = "HW3 Käver for next lecture",
            IsDone = false,
            AppUserId = userForTesting.Id,
        };
        
        Guid guid2 = Guid.NewGuid();
        var todo2 = new Todo()
        {
            Id = new Guid(guid2.ToString()),
            TodoText = "Math for Laur",
            IsDone = true,
            AppUserId = userForTesting.Id,
        };
        
        testBll.Todos.Add(todo1);
        testBll.Todos.Add(todo2);
        await testBll.SaveChangesAsync();
        // To avoid tracing issues
        testBll = GetBLL();
        var result = await testBll.Todos.GetAllAsync();

        // ASSERT
        var todos = result.ToList();
        var firstTodo = todos[0];
        var secondTodo = todos[1];
        _testOutputHelper.WriteLine("First todo: " + firstTodo.TodoText);
        _testOutputHelper.WriteLine("Second todo: " + secondTodo.TodoText);
        Assert.NotEmpty(result);
        Assert.Equal("HW3 Käver for next lecture", firstTodo.TodoText);
        Assert.Equal("Math for Laur", secondTodo.TodoText);

    }
    private async Task<App.BLL.DTO.Identity.AppUser> CreateUserForTesting(string name, string sname)
    {
        var testBll = GetBLL();
        var user = new App.BLL.DTO.Identity.AppUser
        {
            UserName = name + sname,
            FirstName = name,
            LastName = sname,
            Email = name + sname + "@mail.com"
        };
        testBll.AppUsers.Add(user);
        await testBll.SaveChangesAsync();
        var allAsync = await testBll.AppUsers.GetAllAsync();
        var firstOrDefault = allAsync.FirstOrDefault();
        // _testOutputHelper.WriteLine(firstOrDefault!.Id.ToString());
        // // var user = new App.Domain.Identity.AppUser()
        // // {
        // //     Firstname = name,
        // //     Lastname = sname,
        // //     Email = name+sname+"@mail.com"
        // // };
        // // _ctx.AppUsers.Add(user);
        // // await _ctx.SaveChangesAsync();

        return firstOrDefault!;
    }
    
}