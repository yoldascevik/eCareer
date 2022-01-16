using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Cv.Queries.GetById;

public class GetCVByIdQueryHandler: IQueryHandler<GetCVByIdQuery, CVDto>
{
    private readonly IMapper _mapper;
    private readonly ICVRepository _cvRepository;

    public GetCVByIdQueryHandler(ICVRepository cvRepository, IMapper mapper)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<CVDto> Handle(GetCVByIdQuery request, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByKeyAsync(request.CvId);
        if (cv == null || cv.IsDeleted)
        {
            throw new CVNotFoundException(request.CvId);
        }

        return _mapper.Map<CVDto>(cv);
    }
}