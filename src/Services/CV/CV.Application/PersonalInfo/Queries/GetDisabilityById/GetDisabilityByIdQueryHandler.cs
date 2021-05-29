using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.PersonalInfo.Exceptions;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilityById
{
    public class GetDisabilityByIdQueryHandler : IQueryHandler<GetDisabilityByIdQuery, DisabilityDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetDisabilityByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<DisabilityDto> Handle(GetDisabilityByIdQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }

            var disability = cv.PersonalInfo.Disabilities.First(x => x.Id == request.DisabilityId && !x.IsDeleted);
            if (disability == null)
            {
                throw new DisabilityNotFoundException(request.DisabilityId);
            }

            return _mapper.Map<DisabilityDto>(disability);
        }
    }
}