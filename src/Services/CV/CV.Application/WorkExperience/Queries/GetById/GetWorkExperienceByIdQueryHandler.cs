using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.WorkExperience.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.WorkExperience.Queries.GetById;

public class GetWorkExperienceByIdQueryHandler : IQueryHandler<GetWorkExperienceByIdQuery, WorkExperienceDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetWorkExperienceByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<WorkExperienceDto> Handle(GetWorkExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var workExperience = cv.WorkExperiences.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.WorkExperienceId);
        if (workExperience == null)
            throw new WorkExperienceNotFoundException(request.WorkExperienceId);
            
        return _mapper.Map<WorkExperienceDto>(workExperience);
    }
}