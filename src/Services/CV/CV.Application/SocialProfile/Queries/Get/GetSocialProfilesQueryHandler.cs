using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Data.Pagination;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Core.Repositories;
using MongoDB.Bson;

namespace CurriculumVitae.Application.SocialProfile.Queries.Get
{
    public class GetSocialProfilesQueryHandler : IQueryHandler<GetSocialProfilesQuery, List<SocialProfileDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetSocialProfilesQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<SocialProfileDto>> Handle(GetSocialProfilesQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            return _mapper.Map<List<SocialProfileDto>>(cv.SocialProfiles.ExcludeDeletedItems());
        }
    }
}