using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Answer, App.DAL.DTO.Answer>().ReverseMap();
        CreateMap<App.BLL.DTO.Question, App.DAL.DTO.Question>().ReverseMap();
        CreateMap<App.BLL.DTO.Quiz, App.DAL.DTO.Quiz>().ReverseMap();
        CreateMap<App.BLL.DTO.Subject, App.DAL.DTO.Subject>().ReverseMap();
        CreateMap<App.BLL.DTO.Todo, App.DAL.DTO.Todo>().ReverseMap();
        CreateMap<App.BLL.DTO.Topic, App.DAL.DTO.Topic>().ReverseMap();
        CreateMap<App.BLL.DTO.UserChoice, App.DAL.DTO.UserChoice>().ReverseMap();
        CreateMap<App.BLL.DTO.UserComment, App.DAL.DTO.UserComment>().ReverseMap();
        CreateMap<App.BLL.DTO.UserPost, App.DAL.DTO.UserPost>().ReverseMap();
        CreateMap<App.BLL.DTO.UserQuiz, App.DAL.DTO.UserQuiz>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
    }
}