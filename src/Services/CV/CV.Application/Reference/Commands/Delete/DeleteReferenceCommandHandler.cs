using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Reference.Commands.Delete
{
    public class DeleteReferenceCommandHandler : ICommandHandler<DeleteReferenceCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteReferenceCommandHandler> _logger;

        public DeleteReferenceCommandHandler(ICVRepository cvRepository, ILogger<DeleteReferenceCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteReferenceCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);

            var reference = cv.References.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.ReferenceId);
            if (reference == null)
                throw new ReferenceNotFoundException(request.ReferenceId);

            reference.IsDeleted = true;
            await _cvRepository.UpdateAsync(cv.Id, cv);
            
            _logger.LogInformation("Reference \"{ReferenceId}\" is deleted", reference.Id);
            
            return Unit.Value;
        }
    }
}