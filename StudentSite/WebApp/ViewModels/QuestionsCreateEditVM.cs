using App.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class QuestionsCreateEditVM
{
    public Question Question { get; set; } = default!;

    public SelectList? QuizSelectList { get; set; }
}