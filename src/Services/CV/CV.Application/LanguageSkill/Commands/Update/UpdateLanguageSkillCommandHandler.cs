using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Update
{
    public class UpdateLanguageSkillCommandHandler : ICommandHandler<UpdateLanguageSkillCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateLanguageSkillCommandHandler> _logger;

        public UpdateLanguageSkillCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateLanguageSkillCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLanguageSkillCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
            
            var languageSkill = cv.LanguageSkills.FirstOrDefault(x => x.Id == request.LanguageSkillId);
            if (languageSkill == null)
                throw new LanguageSkillNotFoundException(request.LanguageSkillId);
            
            if (request.LanguageSkill.IsPrimary && !languageSkill.IsPrimary)
            {
                cv.LanguageSkills.ToList().ForEach(s => s.IsPrimary = false);
            }

            _mapper.Map(request.LanguageSkill, languageSkill);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Language skill \"{LanguageSkillId}\" updated", request.LanguageSkillId);

            return Unit.Value;
        }
    }
}