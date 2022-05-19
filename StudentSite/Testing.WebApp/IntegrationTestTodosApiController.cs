using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Public.DTO.v1;
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
    private string? _refreshToken;
    
    public IntegrationTestTodosApiController(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
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
    public async Task Main_flow_Quizflow()
    {
        //REGISTER
        var jwt = "";
        var refreshToken = "";
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
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        //refreshtoken
        var refreshTokenModel = new RefreshTokenModel()
        {
            Jwt = jwt,
            RefreshToken = refreshToken
        };
        string jwtTokenInfo = JsonConvert.SerializeObject(refreshTokenModel);

        uri = "/api/v1/identity/account/refreshtoken";
        contentData = new StringContent(jwtTokenInfo, 
            Encoding.UTF8, "application/json");
        response =  _client.PostAsync(uri, contentData).Result;

        response.EnsureSuccessStatusCode();
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<JwtResponse>(content);
            jwt = resp!.Token;
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        // test JWT - Access todos page by jwt
        uri = "/api/v1/Subjects/GetSubjects";

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        var getSubjectsResponse = await _client.GetAsync(uri);

        var getSubjectsNew = await getSubjectsResponse.Content.ReadAsStringAsync();
        var deserializedSubjectsNew = JsonConvert
            .DeserializeObject<List<App.Public.DTO.v1.Subject>>(getSubjectsNew)!;
        Assert.True("Math" == deserializedSubjectsNew[0].Name);
        _testOutputHelper.WriteLine(deserializedSubjectsNew[0].Name);
        
        
        uri = "/api/v1/Subjects/GetSubject/"+deserializedSubjectsNew[0].Id;
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        var getSubjectsResponseWithQuizzes = await _client.GetAsync(uri);

        // ASSERT
        getSubjectsResponseWithQuizzes.EnsureSuccessStatusCode();
        
        getSubjectsNew = await getSubjectsResponseWithQuizzes.Content.ReadAsStringAsync();
        var deserializedOneTopic = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.Subject>(getSubjectsNew)!;
        //TEST IF DATA IS THERE
        var firstQuiz = deserializedOneTopic.Quizzes!.First();
        _testOutputHelper.WriteLine(firstQuiz.Name);
        
        Assert.NotNull(deserializedOneTopic);
        Assert.True("Math Quiz" == firstQuiz.Name);
        var quizForm = new QuizForm()
        {
            QuizId = firstQuiz.Id,
        };
        
        uri = "/api/v1/UserQuiz/PostUserQuiz";

        var postUserQuizResponse = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(quizForm),
                Encoding.UTF8,
                "application/json"));

        postUserQuizResponse.EnsureSuccessStatusCode();

        var getUserQuiz = await postUserQuizResponse.Content.ReadAsStringAsync();
        var deserializedUserQuiz = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.UserQuiz>(getUserQuiz)!;
        _testOutputHelper.WriteLine(deserializedUserQuiz.Id.ToString());
        
        uri = "/api/v1/UserChoice/PostGetUserChoice";
        
        var userQuizForm = new UserQuizForm()
        {
            UserQuizId = deserializedUserQuiz.Id
        };
        var postGetUserChoiceResponse = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(userQuizForm),
                Encoding.UTF8,
                "application/json"));

        postGetUserChoiceResponse.EnsureSuccessStatusCode();
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);
        
        var getPostGetUserChoice = await postGetUserChoiceResponse.Content.ReadAsStringAsync();
        var deserializedPostGetUserChoice = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.UserChoice>(getPostGetUserChoice)!;
        _testOutputHelper.WriteLine(deserializedPostGetUserChoice.QuestionId.ToString());
        
        
        uri = "/api/v1/Question/GetQuestion/"+deserializedPostGetUserChoice.QuestionId;
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        var getQuestionWithAnswers = await _client.GetAsync(uri);

        // ASSERT
        getQuestionWithAnswers.EnsureSuccessStatusCode();
        
        var getQuestionAnswersRead = await getQuestionWithAnswers.Content.ReadAsStringAsync();
        var deserializedQuestionAnswers = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.Question>(getQuestionAnswersRead)!;
        //TEST IF DATA IS THERE
        _testOutputHelper.WriteLine(deserializedQuestionAnswers.QuestionText);
        Assert.NotNull(deserializedQuestionAnswers);
        Assert.True("5+5?" == deserializedQuestionAnswers.QuestionText);
        
        ///Submit user answer
        uri = "/api/v1/UserChoice/PostUserChoice";
        var userAnswerId = deserializedQuestionAnswers.Answers!.First().Id;
        var userChoice = new UserChoice()
        {
            QuizId = firstQuiz.Id,
            UserQuizId = deserializedUserQuiz.Id,
            QuestionId = deserializedQuestionAnswers.Id,
            AnswerId = userAnswerId
        };
        var postPostUserChoice = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(userChoice),
                Encoding.UTF8,
                "application/json"));

        postPostUserChoice.EnsureSuccessStatusCode();
        
        uri = "/api/v1/UserChoice/PostGetUserChoice";
        

        postGetUserChoiceResponse = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(userQuizForm),
                Encoding.UTF8,
                "application/json"));

        postGetUserChoiceResponse.EnsureSuccessStatusCode();
        
         getPostGetUserChoice = await postGetUserChoiceResponse.Content.ReadAsStringAsync();
         deserializedPostGetUserChoice = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.UserChoice>(getPostGetUserChoice)!;
        _testOutputHelper.WriteLine(deserializedPostGetUserChoice.QuestionId.ToString());
        
        uri = "/api/v1/Question/GetQuestion/"+deserializedPostGetUserChoice.QuestionId;
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

         getQuestionWithAnswers = await _client.GetAsync(uri);
         Assert.Equal(HttpStatusCode.NotFound, getQuestionWithAnswers.StatusCode);
         
         
         // then go to results page by UserQuizId
         uri = "/api/v1/UserQuiz/GetUserQuiz/"+deserializedUserQuiz.Id;
        
         _client.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer",
                 _jwt);

         getQuestionWithAnswers = await _client.GetAsync(uri);
         Assert.Equal(HttpStatusCode.OK, getQuestionWithAnswers.StatusCode);
        
    }
    
    
    [Fact]
    public async Task Main_flow_Todos()
    {
        //REGISTER
        var jwt = "";
        var refreshToken = "";
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
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        //refreshtoken
        var refreshTokenModel = new RefreshTokenModel()
        {
            Jwt = jwt,
            RefreshToken = refreshToken
        };
        string jwtTokenInfo = JsonConvert.SerializeObject(refreshTokenModel);

        uri = "/api/v1/identity/account/refreshtoken";
        contentData = new StringContent(jwtTokenInfo, 
            Encoding.UTF8, "application/json");
        response =  _client.PostAsync(uri, contentData).Result;

        response.EnsureSuccessStatusCode();
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<JwtResponse>(content);
            jwt = resp!.Token;
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        // test JWT - Access todos page by jwt
        uri = "/api/v1/todo/GetTodos";

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
    
        [Fact]
    public async Task Main_flow_Posts()
    {
        //REGISTER
        var jwt = "";
        var refreshToken = "";
        // Arrange
        var uri = "/api/v1/identity/Account/Register/";
        var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        Guid guidIdUser = Guid.NewGuid();
        var user = new App.Public.DTO.v1.Identity.Register()
        {
            Id = guidIdUser,
            Email = "test2662@test.com",
            Firstname = "Testa5me",
            Lastname = "Test55er",
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
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        //refreshtoken
        var refreshTokenModel = new RefreshTokenModel()
        {
            Jwt = jwt,
            RefreshToken = refreshToken
        };
        string jwtTokenInfo = JsonConvert.SerializeObject(refreshTokenModel);

        uri = "/api/v1/identity/account/refreshtoken";
        contentData = new StringContent(jwtTokenInfo, 
            Encoding.UTF8, "application/json");
        response =  _client.PostAsync(uri, contentData).Result;

        response.EnsureSuccessStatusCode();
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<JwtResponse>(content);
            jwt = resp!.Token;
            refreshToken = resp!.RefreshToken;
            _jwt = resp.Token;
            _refreshToken = resp.RefreshToken;
        }
        // ASSERT
        Assert.NotEmpty(jwt);
        Assert.NotEmpty(refreshToken);
        
        // test JWT - Access todos page by jwt
        uri = "/api/v1/topic/GetTopics";
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        var getTopicsResponse = await _client.GetAsync(uri);

        // ASSERT
        getTopicsResponse.EnsureSuccessStatusCode();
        
        var getTopics = await getTopicsResponse.Content.ReadAsStringAsync();
        var deserializedTopic = JsonConvert
            .DeserializeObject<List<App.Public.DTO.v1.Topic>>(getTopics)!;
        //TEST IF DATA IS THERE
        Assert.NotEmpty(deserializedTopic);
        Assert.True("America" == deserializedTopic[0].Name);
     
        //ADD POST - POST
        Guid userPostGuidId = Guid.NewGuid();
        var userPost = new App.Public.DTO.v1.UserPost()
        {
            Id = new Guid(userPostGuidId.ToString()),
            Title = "I study here",
            Text = "I realy like studying here in School but you?",
            TopicId = deserializedTopic[0].Id,
            AppUserId = guidIdUser,
            
        };

        uri = "/api/v1/UserPost/PostUserPost";

        var todoResponse = await _client.PostAsync(
            uri,
            new StringContent(
                JsonConvert.SerializeObject(userPost),
                Encoding.UTF8,
                "application/json"));

        todoResponse.EnsureSuccessStatusCode();
        
        
        uri = "/api/v1/topic/GetTopic/"+deserializedTopic[0].Id;
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                _jwt);

        getTopicsResponse = await _client.GetAsync(uri);

        // ASSERT
        getTopicsResponse.EnsureSuccessStatusCode();
        
         getTopics = await getTopicsResponse.Content.ReadAsStringAsync();
         var deserializedOneTopic = JsonConvert
            .DeserializeObject<App.Public.DTO.v1.Topic>(getTopics)!;
        //TEST IF DATA IS THERE
        _testOutputHelper.WriteLine(deserializedOneTopic.UserPosts!.First().Text);
        
        Assert.NotNull(deserializedOneTopic);
        Assert.True("I realy like studying here in School but you?" == deserializedOneTopic.UserPosts!.First().Text);
        

         //ADD comment - POST
         Guid commentGuidId = Guid.NewGuid();
         var userComment = new App.Public.DTO.v1.UserComment()
         {
             Id = new Guid(commentGuidId.ToString()),
             CommentText = "me too btw",
             AppUserId = guidIdUser,
             UserPostId = userPost.Id
         };
        
         uri = "/api/v1/UserComment/PostUserComment";
        
         var commentResponse = await _client.PostAsync(
             uri,
             new StringContent(
                 JsonConvert.SerializeObject(userComment),
                 Encoding.UTF8,
                 "application/json"));
        
         commentResponse.EnsureSuccessStatusCode();
        
        
         //CHECK COMMENT
         uri = "/api/v1/UserPost/GetUserPost/"+userPost.Id;
         _client.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer",
                 _jwt);
        
         var getUserPostsResponse = await _client.GetAsync(uri);
         getUserPostsResponse.EnsureSuccessStatusCode();
        
         var getUserPostWithComments = await getUserPostsResponse.Content.ReadAsStringAsync();
         var deserializedOneUserPosts = JsonConvert
             .DeserializeObject<App.Public.DTO.v1.UserPost>(getUserPostWithComments)!;
        // TEST IF DATA IS THERE
         Assert.NotNull(deserializedOneUserPosts);
         Assert.True("me too btw" == deserializedOneUserPosts.UserComments!.First().CommentText);
        
    }

}