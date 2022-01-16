using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Extensions;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Certificate.Commands.Delete;

public class DeleteCertificateCommandHandler : ICommandHandler<DeleteCertificateCommand>
{
    private readonly ICVRepository _cvRepository;
    private readonly ILogger<DeleteCertificateCommandHandler> _logger;

    public DeleteCertificateCommandHandler(ICVRepository cvRepository, ILogger<DeleteCertificateCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var certificate = cv.Certificates.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.CertificateId);
        if (certificate == null)
            throw new CertificateNotFoundException(request.CertificateId);

        certificate.IsDeleted = true;
        await _cvRepository.UpdateAsync(cv.Id, cv);
            
        _logger.LogInformation("Certificate \"{CertificateId}\" is deleted", certificate.Id);
            
        return Unit.Value;
    }
}