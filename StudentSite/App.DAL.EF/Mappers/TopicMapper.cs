using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TopicMapper: BaseMapper<App.DAL.DTO.Topic, App.Domain.Topic>
{
    public TopicMapper(IMapper mapper) : base(mapper)
    {
    }
}