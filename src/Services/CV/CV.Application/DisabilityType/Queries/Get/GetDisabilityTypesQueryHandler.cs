using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Domain.Extensions;
using Career.MediatR.Query;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Application.DisabilityType.Queries.Get
{
    public class GetDisabilityTypesQueryHandler : IQueryHandler<GetDisabilityTypesQuery, PagedList<DisabilityTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDisabilityTypeRepository _disabilityTypeRepository;

        public GetDisabilityTypesQueryHandler(IDisabilityTypeRepository disabilityTypeRepository, IMapper mapper)
        {
            _disabilityTypeRepository = disabilityTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<DisabilityTypeDto>> Handle(GetDisabilityTypesQuery request, CancellationToken cancellationToken)
        {
            return await _disabilityTypeRepository.Get()
                .ExcludeDeletedItems()
                .OrderBy(x => x.Name)
                .ProjectTo<DisabilityTypeDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request);
        }
    }
}