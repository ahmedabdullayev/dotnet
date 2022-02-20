using System.ComponentModel.DataAnnotations;
using App.Domain.Posts;
using Base.Domain.Identity;
namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    [StringLength(128, MinimumLength = 1)]
    public string Firstname { get; set; } = default!;
    [StringLength(128, MinimumLength = 1)]
    public string Lastname { get; set; } = default!;

    public ICollection<Quiz>? Quizzes { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }
    public ICollection<UserQuiz>? UserQuizzes { get; set; }
    public ICollection<UserPost>? UserPosts { get; set; }
    public ICollection<UserComment>? UserComments { get; set; }
    public ICollection<Todo>? Todos { get; set; }
}
