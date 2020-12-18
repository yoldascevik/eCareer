using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.EducationLevel
{
    public class EducationLevelService : IEducationLevelService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.EducationLevel> _educationLevelRepository;

        public EducationLevelService(
            IMongoRepository<Data.Entities.EducationLevel> educationLevelRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _educationLevelRepository = educationLevelRepository;
        }

        public async Task<PagedList<EducationLevelDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _educationLevelRepository.Get(educationLevel => !educationLevel.IsDeleted)
                .OrderBy(e=> e.Id)
                .ProjectTo<EducationLevelDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<EducationLevelDto> GetByIdAsync(string id)
        {
            var educationLevel = await _educationLevelRepository.GetByKeyAsync(id);
            if (educationLevel == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<EducationLevelDto>(educationLevel);
        }

        public async Task<EducationLevelDto> CreateAsync(EducationLevelRequestModel requestModel)
        {
            await CheckEducationLevelExist(requestModel);
            var createdEducationLevel = await _educationLevelRepository.AddAsync(_mapper.Map<Data.Entities.EducationLevel>(requestModel));

            return _mapper.Map<EducationLevelDto>(createdEducationLevel);
        }

        public async Task<EducationLevelDto> UpdateAsync(string id, EducationLevelRequestModel requestModel)
        {
            if (!await _educationLevelRepository.AnyAsync(educationLevel => educationLevel.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckEducationLevelExist(requestModel, id);

            var updatedEducationLevel = await _educationLevelRepository.UpdateAsync(id, _mapper.Map<Data.Entities.EducationLevel>(requestModel));
            return _mapper.Map<EducationLevelDto>(updatedEducationLevel);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _educationLevelRepository.AnyAsync(educationLevel => educationLevel.Id == id))
                throw new ItemNotFoundException(id);

            await _educationLevelRepository.DeleteAsync(id);
        }

        private async Task CheckEducationLevelExist(EducationLevelRequestModel requestModel, string id = null)
        {
            var existingEducationLevel = await _educationLevelRepository.FirstOrDefaultAsync(educationLevel =>
                (string.IsNullOrEmpty(id) || educationLevel.Id != id)
                && educationLevel.IsDeleted == false
                && educationLevel.Name == requestModel.Name);

            if (existingEducationLevel != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}