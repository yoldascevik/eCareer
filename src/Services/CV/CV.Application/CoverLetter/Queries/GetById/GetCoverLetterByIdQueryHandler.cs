using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.CoverLetter.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.CoverLetter.Queries.GetById
{
    public class GetCoverLetterByIdQueryHandler : IQueryHandler<GetCoverLetterByIdQuery, CoverLetterDto>
    {
        private readonly IMapper _mapper;
        private readonly ICoverLetterRepository _coverLetterRepository;

        public GetCoverLetterByIdQueryHandler(ICoverLetterRepository coverLetterRepository, IMapper mapper)
        {
            _coverLetterRepository = coverLetterRepository;
            _mapper = mapper;
        }

        public async Task<CoverLetterDto> Handle(GetCoverLetterByIdQuery request, CancellationToken cancellationToken)
        {
            var coverLetter = await _coverLetterRepository.GetByKeyAsync(request.CoverLetterId);
            if (coverLetter == null)
            {
                throw new CoverLetterNotFoundException(request.CoverLetterId);
            }

            return _mapper.Map<CoverLetterDto>(coverLetter);
        }
    }
}