using AutoMapper;

namespace App.Public.DTO.v1;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.Public.DTO.v1.Answer, App.BLL.DTO.Answer>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Question, App.BLL.DTO.Question>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Quiz, App.BLL.DTO.Quiz>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Subject, App.BLL.DTO.Subject>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Todo, App.BLL.DTO.Todo>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Topic, App.BLL.DTO.Topic>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserComment, App.BLL.DTO.UserComment>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserPost, App.BLL.DTO.UserPost>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserQuiz, App.BLL.DTO.UserQuiz>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
    }
}