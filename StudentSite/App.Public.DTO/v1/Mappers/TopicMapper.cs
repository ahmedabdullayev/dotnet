using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class TopicMapper: BaseMapper<Public.DTO.v1.Topic, App.BLL.DTO.Topic>
{
    public TopicMapper(IMapper mapper) : base(mapper)
    {
    }
}