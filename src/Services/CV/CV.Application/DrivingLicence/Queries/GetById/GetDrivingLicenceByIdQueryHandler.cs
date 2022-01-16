using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv;
using CurriculumVitae.Application.DrivingLicence.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.DrivingLicence.Queries.GetById;

public class GetDrivingLicenceByIdQueryHandler : IQueryHandler<GetDrivingLicenceByIdQuery, DrivingLicenceDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetDrivingLicenceByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<DrivingLicenceDto> Handle(GetDrivingLicenceByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        var drivingLicence = cv.DrivingLicences.ExcludeDeletedItems().FirstOrDefault(x => x.Id == request.DrivingLicenceId);
        if (drivingLicence == null)
        {
            throw new DrivingLicenceNotFoundException(request.DrivingLicenceId);
        }
            
        return _mapper.Map<DrivingLicenceDto>(drivingLicence);
    }
}