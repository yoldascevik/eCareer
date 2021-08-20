using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Command;
using Career.Shared.Generators;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Certificate.Commands.Add
{
    public class AddCertificateCommandHandler : ICommandHandler<AddCertificateCommand, CertificateDto>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;
        private readonly IStringIdGenerator _stringIdGenerator;
        private readonly ILogger<AddCertificateCommandHandler> _logger;

        public AddCertificateCommandHandler(
            ICVRepository cvRepository, 
            IMapper mapper, 
            IStringIdGenerator stringIdGenerator,
            ILogger<AddCertificateCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _cvRepository = cvRepository;
            _stringIdGenerator = stringIdGenerator;
        }

        public async Task<CertificateDto> Handle(AddCertificateCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
                throw new CVNotFoundException(request.CvId);
  
            var certificate = _mapper.Map<Core.Entities.Certificate>(request.Certificate);
            certificate.Id = _stringIdGenerator.Generate();

            cv.Certificates.Add(certificate);

            await _cvRepository.UpdateAsync(cv.Id, cv);
            _logger.LogInformation("New certificate ({CertificateId}) added to CV ({CvId})", certificate.Id, cv.Id);

            return _mapper.Map<CertificateDto>(certificate);
        }
    }
}