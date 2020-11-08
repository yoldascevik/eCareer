using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Career.Utilities.Pagination;

namespace Definition.Application.Work.WorkType
{
    public class WorkTypeService : IWorkTypeService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.WorkType> _workTypeRepository;

        public WorkTypeService(
            IMongoRepository<Data.Entities.WorkType> workTypeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _workTypeRepository = workTypeRepository;
        }

        public async Task<PagedList<WorkTypeDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _workTypeRepository.Get(workType => !workType.IsDeleted)
                .ProjectTo<WorkTypeDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<WorkTypeDto> GetByIdAsync(string id)
        {
            var workType = await _workTypeRepository.GetByKeyAsync(id);
            if (workType == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<WorkTypeDto>(workType);
        }

        public async Task<WorkTypeDto> CreateAsync(WorkTypeRequestModel requestModel)
        {
            await CheckWorkTypeExist(requestModel);
            var createdWorkType = await _workTypeRepository.AddAsync(_mapper.Map<Data.Entities.WorkType>(requestModel));

            return _mapper.Map<WorkTypeDto>(createdWorkType);
        }

        public async Task<WorkTypeDto> UpdateAsync(string id, WorkTypeRequestModel requestModel)
        {
            if (!await _workTypeRepository.AnyAsync(workType => workType.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckWorkTypeExist(requestModel, id);

            var updatedWorkType = await _workTypeRepository.UpdateAsync(id, _mapper.Map<Data.Entities.WorkType>(requestModel));
            return _mapper.Map<WorkTypeDto>(updatedWorkType);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _workTypeRepository.AnyAsync(workType => workType.Id == id))
                throw new ItemNotFoundException(id);

            await _workTypeRepository.DeleteAsync(id);
        }

        private async Task CheckWorkTypeExist(WorkTypeRequestModel requestModel, string id = null)
        {
            var existingWorkType = await _workTypeRepository.FirstOrDefaultAsync(workType =>
                (string.IsNullOrEmpty(id) || workType.Id != id)
                && workType.IsDeleted == false
                && workType.Name == requestModel.Name);

            if (existingWorkType != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}