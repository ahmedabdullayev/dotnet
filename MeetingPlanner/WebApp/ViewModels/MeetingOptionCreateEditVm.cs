using App.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class MeetingOptionCreateEditVM
{
    public MeetingOption MeetingOption { get; set; } = default!;

    public SelectList? MeetingSelectList { get; set; }
}