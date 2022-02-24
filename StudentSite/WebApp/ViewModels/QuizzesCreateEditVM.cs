using App.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class QuizzesCreateEditVM
{
    public Quiz Quiz { get; set; } = default!;
    public SelectList? SubjectSelectList { get; set; }
}