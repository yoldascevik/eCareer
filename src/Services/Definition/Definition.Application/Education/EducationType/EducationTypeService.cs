using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Career.Utilities.Pagination;

namespace Definition.Application.Education.EducationType
{
    public class EducationTypeService : IEducationTypeService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.EducationType> _educationTypeRepository;

        public EducationTypeService(
            IMongoRepository<Data.Entities.EducationType> educationTypeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _educationTypeRepository = educationTypeRepository;
        }

        public async Task<PagedList<EducationTypeDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _educationTypeRepository.Get(educationType => !educationType.IsDeleted)
                .ProjectTo<EducationTypeDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<EducationTypeDto> GetByIdAsync(string id)
        {
            var educationType = await _educationTypeRepository.GetByKeyAsync(id);
            if (educationType == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<EducationTypeDto>(educationType);
        }

        public async Task<EducationTypeDto> CreateAsync(EducationTypeRequestModel requestModel)
        {
            await CheckEducationTypeExist(requestModel);
            var createdEducationType = await _educationTypeRepository.AddAsync(_mapper.Map<Data.Entities.EducationType>(requestModel));

            return _mapper.Map<EducationTypeDto>(createdEducationType);
        }

        public async Task<EducationTypeDto> UpdateAsync(string id, EducationTypeRequestModel requestModel)
        {
            if (!await _educationTypeRepository.AnyAsync(educationType => educationType.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckEducationTypeExist(requestModel, id);

            var updatedEducationType = await _educationTypeRepository.UpdateAsync(id, _mapper.Map<Data.Entities.EducationType>(requestModel));
            return _mapper.Map<EducationTypeDto>(updatedEducationType);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _educationTypeRepository.AnyAsync(educationType => educationType.Id == id))
                throw new ItemNotFoundException(id);

            await _educationTypeRepository.DeleteAsync(id);
        }

        private async Task CheckEducationTypeExist(EducationTypeRequestModel requestModel, string id = null)
        {
            var existingEducationType = await _educationTypeRepository.FirstOrDefaultAsync(educationType =>
                (string.IsNullOrEmpty(id) || educationType.Id != id)
                && educationType.IsDeleted == false
                && educationType.Name == requestModel.Name);

            if (existingEducationType != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}