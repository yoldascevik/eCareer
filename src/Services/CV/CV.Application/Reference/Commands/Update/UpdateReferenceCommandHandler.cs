using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.Exceptions.Exceptions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Reference.Commands.Update
{
    public class UpdateReferenceCommandHandler : ICommandHandler<UpdateReferenceCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateReferenceCommandHandler> _logger;

        public UpdateReferenceCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateReferenceCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReferenceCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
            
            var reference = cv.References.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.ReferenceId);
            if (reference == null)
                throw new ReferenceNotFoundException(request.ReferenceId);

            _mapper.Map(request.Reference, reference);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Reference \"{ReferenceId}\" updated", request.ReferenceId);

            return Unit.Value;
        }
    }
}