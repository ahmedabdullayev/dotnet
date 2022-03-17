using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class SubjectsDTO : DomainEntityId
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

}