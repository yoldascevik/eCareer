using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Career.Utilities.Pagination;

namespace Definition.Application.Work.JobPosition
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.JobPosition> _jobPositionRepository;

        public JobPositionService(
            IMongoRepository<Data.Entities.JobPosition> jobPositionRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<PagedList<JobPositionDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _jobPositionRepository.Get(jobPosition => !jobPosition.IsDeleted)
                .ProjectTo<JobPositionDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }
        
        public async Task<JobPositionDto> GetByIdAsync(string id)
        {
            var jobPosition = await _jobPositionRepository.GetByKeyAsync(id);
            if (jobPosition == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<JobPositionDto>(jobPosition);
        }

        public async Task<JobPositionDto> CreateAsync(JobPositionRequestModel requestModel)
        {
            await CheckJobPositionExist(requestModel);
            var createdJobPosition = await _jobPositionRepository.AddAsync(_mapper.Map<Data.Entities.JobPosition>(requestModel));

            return _mapper.Map<JobPositionDto>(createdJobPosition);
        }

        public async Task<JobPositionDto> UpdateAsync(string id, JobPositionRequestModel requestModel)
        {
            if (!await _jobPositionRepository.AnyAsync(jobPosition => jobPosition.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckJobPositionExist(requestModel, id);

            var updatedJobPosition = await _jobPositionRepository.UpdateAsync(id, _mapper.Map<Data.Entities.JobPosition>(requestModel));
            return _mapper.Map<JobPositionDto>(updatedJobPosition);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _jobPositionRepository.AnyAsync(jobPosition => jobPosition.Id == id))
                throw new ItemNotFoundException(id);

            await _jobPositionRepository.DeleteAsync(id);
        }

        private async Task CheckJobPositionExist(JobPositionRequestModel requestModel, string id = null)
        {
            var existingJobPosition = await _jobPositionRepository.FirstOrDefaultAsync(jobPosition =>
                (string.IsNullOrEmpty(id) || jobPosition.Id != id)
                && jobPosition.IsDeleted == false
                && jobPosition.SectorId == requestModel.SectorId
                && jobPosition.Name == requestModel.Name);

            if (existingJobPosition != null)
                throw new ItemAlreadyExistsException(requestModel.Name);
        }
    }
}