using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Reference.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Reference.Queries.Get;

public class GetReferencesQueryHandler : IQueryHandler<GetReferencesQuery, List<ReferenceDto>>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetReferencesQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<List<ReferenceDto>> Handle(GetReferencesQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        return _mapper.Map<List<ReferenceDto>>(cv.References.ExcludeDeletedItems());
    }
}