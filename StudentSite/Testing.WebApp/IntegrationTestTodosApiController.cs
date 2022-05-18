using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Public.DTO.v1.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using AppUser = App.Domain.Identity.AppUser;

namespace Testing.WebApp;

public class IntegrationTestTodosApiController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private string? _jwt;
    
    public IntegrationTestTodosApiController(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
            })
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
    }
    
    
    [Fact]
    public async void Test_Get_Register_Returns_Method_Not_Allowed()
    {
        var response = await _client.GetAsync("api/v1.0/identity/account/register");

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }
    [Fact]
    public async Task Get_Todos_API_Returns_Unauthorized()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/Todo/GetTodos");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async Task Main_flow()
    {
        //REGISTER
        var jwt = "";
        // Arrange
        var uri = "/api/v1/identity/Account/Register/";
        var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        Guid guidIdUser = Guid.NewGuid();
        var user = new App.Public.DTO.v1.Identity.Register()
        {
            Id = guidIdUser,
            Email = "test22@test.com",
            Firstname = "Testame",
            Lastname = "Tester",
            Password = "123456"
        };
        
        string userInfo = JsonConvert.SerializeObject(user);
        var contentData = new StringContent(userInfo, 
            Encoding.UTF8, "application/json");
        
        HttpResponseMessage response =  _client.PostAsync(uri, contentData).Result;

        response.EnsureSuccessStatusCode();
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<JwtResponse>(content);
            jwt = resp!.Token;
            _jwt = resp.Token;
        }
        // ASSERT
        Assert.NotEmpty(jwt);

        uri = "/api/v1/todo/GetTodos";

        // test JWT - Access todos page by jwt
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        var getTodosResponse = await _client.GetAsync(uri);

        // ASSERT
        getTodosResponse.EnsureSuccessStatusCode();
        
        //ADD TODOo - POST
        Guid guidId = Guid.NewGuid();
        var todo = new App.Public.DTO.v1.Todo
        {
            Id = new Guid(guidId.ToString()),
            TodoText = "HW3 Kaver",
            IsDone = true,
            AppUserId = guidIdUser
        };
            
        uri = "/api/v1/todo/PostTodo";

        var todoResponse = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(todo),
                Encoding.UTF8,
                "application/json"));

        todoResponse.EnsureSuccessStatusCode();
        
        
        uri = "/api/v1/todo/GetTodos";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        getTodosResponse = await _client.GetAsync(uri);
        getTodosResponse.EnsureSuccessStatusCode();
        
        var getTodos = await getTodosResponse.Content.ReadAsStringAsync();
        var deserializedTodo = JsonConvert
            .DeserializeObject<List<App.Public.DTO.v1.Todo>>(getTodos)!;
        //TEST IF DATA IS THERE
        Assert.NotEmpty(deserializedTodo);
        Assert.True("HW3 Kaver" == deserializedTodo[0].TodoText);
        
        //EDIT TODOo DATA
        var todoToEdit = deserializedTodo[0];
        _testOutputHelper.WriteLine(todoToEdit.Id.ToString());
        todoToEdit.TodoText = "DoMath";
        todoToEdit.IsDone = false;
        _testOutputHelper.WriteLine(todoToEdit.TodoText);
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);
        var uriNew = "/api/v1/Todo/PutTodo/" + todoToEdit.Id;
        var updateTodoResponse = await _client.PutAsync(
            uriNew,
            new StringContent(
                JsonConvert.SerializeObject(todoToEdit),
                System.Text.Encoding.UTF8,
                "application/json"));
        //
        updateTodoResponse.EnsureSuccessStatusCode();
        
        uri = "/api/v1/todo/GetTodos";
        var getTodosResponseNew = await _client.GetAsync(uri);
        getTodosResponseNew.EnsureSuccessStatusCode();
        var getTodosNew = await getTodosResponseNew.Content.ReadAsStringAsync();
        var deserializedTodoNew = JsonConvert
        .DeserializeObject<List<App.Public.DTO.v1.Todo>>(getTodosNew)!;
        _testOutputHelper.WriteLine(deserializedTodoNew[0].TodoText);
        // ASERT CHECK
        Assert.NotEmpty(deserializedTodoNew);
        Assert.True("DoMath" == deserializedTodoNew[0].TodoText);
        Assert.False(deserializedTodoNew[0].IsDone);
        
        //Delete 
        
        uri = "/api/v1/Todo/DeleteTodo/" + deserializedTodoNew[0].Id;
            
        var deletedQuestionnaireResponse = await _client.DeleteAsync(
            uri);
        deletedQuestionnaireResponse.EnsureSuccessStatusCode();
        
        uri = "/api/v1/todo/GetTodos";
         getTodosResponseNew = await _client.GetAsync(uri);
        getTodosResponseNew.EnsureSuccessStatusCode();
         getTodosNew = await getTodosResponseNew.Content.ReadAsStringAsync();
         deserializedTodoNew = JsonConvert
            .DeserializeObject<List<App.Public.DTO.v1.Todo>>(getTodosNew)!;
        // ASERT CHECK
        Assert.Empty(deserializedTodoNew);
    }

}