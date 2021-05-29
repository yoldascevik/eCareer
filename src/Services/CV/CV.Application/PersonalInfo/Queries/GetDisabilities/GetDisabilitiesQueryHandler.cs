using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilities
{
    public class GetDisabilitiesQueryHandler : IQueryHandler<GetDisabilitiesQuery, List<DisabilityDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetDisabilitiesQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<DisabilityDto>> Handle(GetDisabilitiesQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }
            
            return _mapper.Map<List<DisabilityDto>>(cv.PersonalInfo.Disabilities.ExcludeDeletedItems());
        }
    }
}