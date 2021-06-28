using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.WorkExperience.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.WorkExperience.Queries.Get
{
    public class GetWorkExperiencesQueryHandler : IQueryHandler<GetWorkExperiencesQuery, List<WorkExperienceDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetWorkExperiencesQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkExperienceDto>> Handle(GetWorkExperiencesQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);

            return _mapper.Map<List<WorkExperienceDto>>(cv.WorkExperiences.ExcludeDeletedItems());
        }
    }
}