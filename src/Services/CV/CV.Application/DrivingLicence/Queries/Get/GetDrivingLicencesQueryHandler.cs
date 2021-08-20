using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.DrivingLicence.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.DrivingLicence.Queries.Get
{
    public class GetDrivingLicencesQueryHandler : IQueryHandler<GetDrivingLicencesQuery, List<DrivingLicenceDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetDrivingLicencesQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<DrivingLicenceDto>> Handle(GetDrivingLicencesQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            return _mapper.Map<List<DrivingLicenceDto>>(cv.DrivingLicences.ExcludeDeletedItems());
        }
    }
}