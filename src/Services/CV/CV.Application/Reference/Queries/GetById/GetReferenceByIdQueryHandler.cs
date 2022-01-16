using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Reference.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Reference.Queries.GetById;

public class GetReferenceByIdQueryHandler : IQueryHandler<GetReferenceByIdQuery, ReferenceDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetReferenceByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<ReferenceDto> Handle(GetReferenceByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var reference = cv.References.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.ReferenceId);
        if (reference == null)
            throw new ReferenceNotFoundException(request.ReferenceId);
            
        return _mapper.Map<ReferenceDto>(reference);
    }
}