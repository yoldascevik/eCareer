using AutoMapper;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.LanguageSkill.Commands.Add;

public class AddLanguageSkillCommandHandler : ICommandHandler<AddLanguageSkillCommand, LanguageSkillDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;
    private readonly IStringIdGenerator _stringIdGenerator;
    private readonly ILogger<AddLanguageSkillCommandHandler> _logger;

    public AddLanguageSkillCommandHandler(
        ICVRepository cvRepository,
        IMapper mapper,
        IStringIdGenerator stringIdGenerator,
        ILogger<AddLanguageSkillCommandHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _cvRepository = cvRepository;
        _stringIdGenerator = stringIdGenerator;
    }

    public async Task<LanguageSkillDto> Handle(AddLanguageSkillCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        if (cv.LanguageSkills.Any(x => x.Language.Id == request.LanguageSkill.Language.Id))
            throw new ItemAlreadyExistsException(request.LanguageSkill.Language.Name);

        var languageSkill = _mapper.Map<Core.Entities.LanguageSkill>(request.LanguageSkill);
        languageSkill.Id = _stringIdGenerator.Generate();

        if (languageSkill.IsPrimary)
        {
            cv.LanguageSkills.ToList().ForEach(s => s.IsPrimary = false);
        }
        else if (!cv.LanguageSkills.Any(x => x.IsPrimary))
        {
            languageSkill.IsPrimary = true;
        }

        cv.LanguageSkills.Add(languageSkill);

        await _cvRepository.UpdateAsync(cv.Id, cv);
        _logger.LogInformation("New language skill ({LanguageName}) added to CV ({CvId})", languageSkill.Language.Name, cv.Id);

        return _mapper.Map<LanguageSkillDto>(languageSkill);
    }
}