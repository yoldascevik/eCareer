using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.DisabilityType.Queries.GetById;

public class GetDisabilityTypeByIdQueryHandler : IQueryHandler<GetDisabilityTypeByIdQuery, DisabilityTypeDto>
{
    private readonly IMapper _mapper;
    private readonly IDisabilityTypeRepository _disabilityTypeRepository;

    public GetDisabilityTypeByIdQueryHandler(IDisabilityTypeRepository disabilityTypeRepository, IMapper mapper)
    {
        _disabilityTypeRepository = disabilityTypeRepository;
        _mapper = mapper;
    }

    public async Task<DisabilityTypeDto> Handle(GetDisabilityTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var disabilityType = await _disabilityTypeRepository.GetByKeyAsync(request.DisabilityTypeId);
        if (disabilityType == null || disabilityType.IsDeleted)
            throw new DisabilityTypeNotFoundException(request.DisabilityTypeId);

        return _mapper.Map<DisabilityTypeDto>(disabilityType);
    }
}