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

namespace CurriculumVitae.Application.Certificate.Commands.Update
{
    public class UpdateCertificateCommandHandler : ICommandHandler<UpdateCertificateCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<UpdateCertificateCommandHandler> _logger;

        public UpdateCertificateCommandHandler(ICVRepository cvRepository, IMapper mapper, ILogger<UpdateCertificateCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
            
            var certificate = cv.Certificates.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.CertificateId);
            if (certificate == null)
                throw new CertificateNotFoundException(request.CertificateId);

            _mapper.Map(request.Certificate, certificate);
            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("Certificate \"{CertificateId}\" updated", request.CertificateId);

            return Unit.Value;
        }
    }
}