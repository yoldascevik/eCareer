using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Education.Commands.Update
{
    public class UpdateEducationCommandHandler : ICommandHandler<UpdateEducationCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateEducationCommandHandler> _logger;

        public UpdateEducationCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateEducationCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
            
            var education = cv.Educations.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.EducationId);
            if (education == null)
                throw new EducationNotFoundException(request.EducationId);

            _mapper.Map(request.Education, education);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Education \"{EducationId}\" updated", request.EducationId);

            return Unit.Value;
        }
    }
}