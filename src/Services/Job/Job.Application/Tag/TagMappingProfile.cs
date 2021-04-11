using AutoMapper;
using Job.Application.Tag.Dtos;

namespace Job.Application.Tag
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Domain.TagAggregate.Tag, TagDto>();
        }
    }
}