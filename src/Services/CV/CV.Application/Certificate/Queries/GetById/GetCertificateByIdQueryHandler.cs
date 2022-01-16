using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Certificate.Queries.GetById;

public class GetCertificateByIdQueryHandler : IQueryHandler<GetCertificateByIdQuery, CertificateDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetCertificateByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<CertificateDto> Handle(GetCertificateByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        var certificate = cv.Certificates.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.CertificateId);
        if (certificate == null)
            throw new CertificateNotFoundException(request.CertificateId);
            
        return _mapper.Map<CertificateDto>(certificate);
    }
}