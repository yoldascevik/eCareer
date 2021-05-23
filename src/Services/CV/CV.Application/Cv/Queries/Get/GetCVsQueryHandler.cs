using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.MediatR.Query;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.Cv.Queries.Get
{
    public class GetCVsQueryHandler: IQueryHandler<GetCVsQuery, PagedList<CVSummaryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICVRepository _cvRepository;

        public GetCVsQueryHandler(ICVRepository cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CVSummaryDto>> Handle(GetCVsQuery request, CancellationToken cancellationToken)
        {
            PagedList<CVSummaryDto> cvList = await _cvRepository.Get(x => !x.IsDeleted)
                .OrderBy(x => x.PersonalInfo.FirstName)
                .ProjectTo<CVSummaryDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request);

            return cvList;
        }
    }
}