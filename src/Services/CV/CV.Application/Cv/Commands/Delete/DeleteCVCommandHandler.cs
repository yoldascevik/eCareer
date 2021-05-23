using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Cv.Commands.Delete
{
    public class DeleteCVCommandHandler: ICommandHandler<DeleteCVCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteCVCommandHandler> _logger;

        public DeleteCVCommandHandler(ICVRepository cvRepository, ILogger<DeleteCVCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCVCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.Id);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.Id);
            }

            await _cvRepository.DeleteAsync(cv.Id);
            _logger.LogInformation("CV {FirstName} {LastName} ({CvId}) is deleted", cv.PersonalInfo.FirstName, cv.PersonalInfo.LastName, cv.Id);
            
            return Unit.Value;
        }
    }
}