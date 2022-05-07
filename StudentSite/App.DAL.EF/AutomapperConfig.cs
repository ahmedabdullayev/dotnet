using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.DAL.DTO.Answer, App.Domain.Answer>().ReverseMap();
        CreateMap<App.DAL.DTO.Question, App.Domain.Question>().ReverseMap();
        CreateMap<App.DAL.DTO.Quiz, App.Domain.Quiz>().ReverseMap();
        CreateMap<App.DAL.DTO.Subject, App.Domain.Subject>().ReverseMap();
        CreateMap<App.DAL.DTO.Todo, App.Domain.Todo>().ReverseMap();
        CreateMap<App.DAL.DTO.Topic, App.Domain.Topic>().ReverseMap();
        CreateMap<App.DAL.DTO.UserChoice, App.Domain.UserChoice>().ReverseMap();
        CreateMap<App.DAL.DTO.UserComment, App.Domain.UserComment>().ReverseMap();
        CreateMap<App.DAL.DTO.UserPost, App.Domain.UserPost>().ReverseMap();
        CreateMap<App.DAL.DTO.UserQuiz, App.Domain.UserQuiz>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
    }
}