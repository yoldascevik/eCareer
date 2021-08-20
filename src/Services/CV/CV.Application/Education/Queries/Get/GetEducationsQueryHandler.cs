using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Education.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Education.Queries.Get
{
    public class GetEducationsQueryHandler : IQueryHandler<GetEducationsQuery, List<EducationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetEducationsQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<EducationDto>> Handle(GetEducationsQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);

            return _mapper.Map<List<EducationDto>>(cv.Educations.ExcludeDeletedItems());
        }
    }
}