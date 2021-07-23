using AutoMapper;
using CurriculumVitae.Application.Attachment.Dtos;

namespace CurriculumVitae.Application.Attachment
{
    public class AttachmentMappingProfile : Profile
    {
        public AttachmentMappingProfile()
        {
            CreateMap<Core.Entities.Attachment, AttachmentDto>();
            CreateMap<AttachmentInputDto, Core.Entities.Attachment>();
        }
    }
}