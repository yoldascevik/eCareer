using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Delete
{
    public class DeleteLanguageSkillCommandHandler : ICommandHandler<DeleteLanguageSkillCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteLanguageSkillCommandHandler> _logger;

        public DeleteLanguageSkillCommandHandler(ICVRepository cvRepository, ILogger<DeleteLanguageSkillCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteLanguageSkillCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);

            var languageSkill = cv.LanguageSkills.FirstOrDefault(x => x.Id == request.LanguageSkillId);
            if (languageSkill == null)
                throw new LanguageSkillNotFoundException(request.LanguageSkillId);

            if (languageSkill.IsPrimary)
                throw new BusinessException("Primary language skill can not be deleted!");

            cv.LanguageSkills.Remove(languageSkill);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            
            _logger.LogInformation("Language skill \"{Language}\" is deleted", languageSkill.Language.Name);
            
            return Unit.Value;
        }
    }
}