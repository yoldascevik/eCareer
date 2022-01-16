using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Certificate.Queries.Get;

public class GetCertificatesQueryHandler : IQueryHandler<GetCertificatesQuery, List<CertificateDto>>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetCertificatesQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<List<CertificateDto>> Handle(GetCertificatesQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
            throw new CVNotFoundException(request.CvId);

        return _mapper.Map<List<CertificateDto>>(cv.Certificates.ExcludeDeletedItems());
    }
}