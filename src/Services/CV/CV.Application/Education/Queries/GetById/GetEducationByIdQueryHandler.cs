using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Education.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Education.Queries.GetById;

public class GetEducationByIdQueryHandler : IQueryHandler<GetEducationByIdQuery, EducationDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetEducationByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<EducationDto> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var education = cv.Educations.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.EducationId);
        if (education == null)
            throw new EducationNotFoundException(request.EducationId);
            
        return _mapper.Map<EducationDto>(education);
    }
}