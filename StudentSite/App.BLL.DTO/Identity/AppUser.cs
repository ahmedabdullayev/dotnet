using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.BLL.DTO.Identity;

public class AppUser: BaseUser
{
    [StringLength(128)] public string FirstName { get; set; } = default!;
    [StringLength(128)] public string LastName { get; set; } = default!;
    
    // public ICollection<Quiz>? Quizzes { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }
    public ICollection<UserQuiz>? UserQuizzes { get; set; }
    public ICollection<UserPost>? UserPosts { get; set; }
    public ICollection<UserComment>? UserComments { get; set; }
    public ICollection<Todo>? Todos { get; set; }

    
}