using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.ScholarshipType
{
    public class ScholarshipTypeService : IScholarshipTypeService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.ScholarshipType> _scholarshipTypeRepository;

        public ScholarshipTypeService(
            IMongoRepository<Data.Entities.ScholarshipType> scholarshipTypeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _scholarshipTypeRepository = scholarshipTypeRepository;
        }

        public async Task<PagedList<ScholarshipTypeDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _scholarshipTypeRepository.Get(scholarshipType => !scholarshipType.IsDeleted)
                .OrderBy(s=> s.Id)
                .ProjectTo<ScholarshipTypeDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<ScholarshipTypeDto> GetByIdAsync(string id)
        {
            var scholarshipType = await _scholarshipTypeRepository.GetByKeyAsync(id);
            if (scholarshipType == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<ScholarshipTypeDto>(scholarshipType);
        }

        public async Task<ScholarshipTypeDto> CreateAsync(ScholarshipTypeRequestModel requestModel)
        {
            await CheckScholarshipTypeExist(requestModel);
            var createdScholarshipType = await _scholarshipTypeRepository.AddAsync(_mapper.Map<Data.Entities.ScholarshipType>(requestModel));

            return _mapper.Map<ScholarshipTypeDto>(createdScholarshipType);
        }

        public async Task<ScholarshipTypeDto> UpdateAsync(string id, ScholarshipTypeRequestModel requestModel)
        {
            if (!await _scholarshipTypeRepository.AnyAsync(scholarshipType => scholarshipType.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckScholarshipTypeExist(requestModel, id);

            var updatedScholarshipType = await _scholarshipTypeRepository.UpdateAsync(id, _mapper.Map<Data.Entities.ScholarshipType>(requestModel));
            return _mapper.Map<ScholarshipTypeDto>(updatedScholarshipType);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _scholarshipTypeRepository.AnyAsync(scholarshipType => scholarshipType.Id == id))
                throw new ItemNotFoundException(id);

            await _scholarshipTypeRepository.DeleteAsync(id);
        }

        private async Task CheckScholarshipTypeExist(ScholarshipTypeRequestModel requestModel, string id = null)
        {
            var existingScholarshipType = await _scholarshipTypeRepository.FirstOrDefaultAsync(scholarshipType =>
                (string.IsNullOrEmpty(id) || scholarshipType.Id != id)
                && scholarshipType.IsDeleted == false
                && scholarshipType.Name == requestModel.Name);

            if (existingScholarshipType != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}