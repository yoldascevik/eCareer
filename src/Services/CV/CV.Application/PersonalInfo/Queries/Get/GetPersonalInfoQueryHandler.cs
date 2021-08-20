using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.PersonalInfo.Queries.Get
{
    public class GetPersonalInfoQueryHandler : IQueryHandler<GetPersonalInfoQuery, PersonalInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetPersonalInfoQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<PersonalInfoDto> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            return _mapper.Map<PersonalInfoDto>(cv.PersonalInfo);
        }
    }
}