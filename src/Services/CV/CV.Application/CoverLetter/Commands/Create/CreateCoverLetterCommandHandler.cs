using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.CoverLetter.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.CoverLetter.Commands.Create
{
    public class CreateCoverLetterCommandHandler : ICommandHandler<CreateCoverLetterCommand, CoverLetterDto>
    {
        private readonly IMapper _mapper;
        private readonly ICoverLetterRepository _coverLetterRepository;
        private readonly ILogger<CreateCoverLetterCommandHandler> _logger;

        public CreateCoverLetterCommandHandler(ICoverLetterRepository coverLetterRepository, IMapper mapper, ILogger<CreateCoverLetterCommandHandler> logger)
        {
            _coverLetterRepository = coverLetterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CoverLetterDto> Handle(CreateCoverLetterCommand request, CancellationToken cancellationToken)
        {
            var coverLetter = await _coverLetterRepository.AddAsync(_mapper.Map<Core.Entities.CoverLetter>(request));

            _logger.LogInformation("Cover letter \"{Title}\" created by user \"{UserId}\"", coverLetter.Title,  coverLetter.UserId);
            
            return _mapper.Map<CoverLetterDto>(coverLetter);
        }
    }
}