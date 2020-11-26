using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.Sector
{
    public class SectorService : ISectorService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.Sector> _sectorRepository;

        public SectorService(
            IMongoRepository<Data.Entities.Sector> sectorRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _sectorRepository = sectorRepository;
        }

        public async Task<PagedList<SectorDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _sectorRepository.Get(sector => !sector.IsDeleted)
                .ProjectTo<SectorDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<SectorDto> GetByIdAsync(string id)
        {
            var sector = await _sectorRepository.GetByKeyAsync(id);
            if (sector == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<SectorDto>(sector);
        }

        public async Task<SectorDto> CreateAsync(SectorRequestModel requestModel)
        {
            await CheckSectorExist(requestModel);
            var createdSector = await _sectorRepository.AddAsync(_mapper.Map<Data.Entities.Sector>(requestModel));

            return _mapper.Map<SectorDto>(createdSector);
        }

        public async Task<SectorDto> UpdateAsync(string id, SectorRequestModel requestModel)
        {
            if (!await _sectorRepository.AnyAsync(sector => sector.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckSectorExist(requestModel, id);

            var updatedSector = await _sectorRepository.UpdateAsync(id, _mapper.Map<Data.Entities.Sector>(requestModel));
            return _mapper.Map<SectorDto>(updatedSector);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _sectorRepository.AnyAsync(sector => sector.Id == id))
                throw new ItemNotFoundException(id);

            await _sectorRepository.DeleteAsync(id);
        }

        private async Task CheckSectorExist(SectorRequestModel requestModel, string id = null)
        {
            var existingSector = await _sectorRepository.FirstOrDefaultAsync(sector =>
                (string.IsNullOrEmpty(id) || sector.Id != id)
                && sector.IsDeleted == false
                && sector.Name == requestModel.Name);

            if (existingSector != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}