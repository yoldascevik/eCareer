using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Location.District;

public class DistrictService : IDistrictService
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<Data.Entities.District> _districtRepository;
    private readonly IMongoRepository<Data.Entities.City> _cityRepository;

    public DistrictService(
        IMongoRepository<Data.Entities.District> districtRepository,
        IMongoRepository<Data.Entities.City> cityRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _districtRepository = districtRepository;
        _cityRepository = cityRepository;
    }

    public async Task<PagedList<DistrictDto>> GetAsync(PaginationFilter paginationFilter)
    {
        return await _districtRepository.Get(district => !district.IsDeleted)
            .OrderBy(d => d.Id)
            .ProjectTo<DistrictDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationFilter);
    }

    public async Task<PagedList<DistrictDto>> GetByCityId(string cityId, PaginationFilter paginationFilter)
    {
        if (!await _cityRepository.AnyAsync(city => city.Id == cityId))
            throw new NotFoundException($"City not found for Id:{cityId}");

        return await _districtRepository.Get(district => district.CityId == cityId && !district.IsDeleted)
            .OrderBy(d => d.Id)
            .ProjectTo<DistrictDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationFilter);
    }

    public async Task<DistrictDto> GetByIdAsync(string id)
    {
        var district = await _districtRepository.GetByKeyAsync(id);
        if (district == null)
            throw new ItemNotFoundException(id);

        return _mapper.Map<DistrictDto>(district);
    }

    public async Task<DistrictDto> CreateAsync(DistrictRequestModel requestModel)
    {
        Data.Entities.City city = await _cityRepository.FirstOrDefaultAsync(c => c.Id == requestModel.CityId);
        if (city == null)
            throw new NotFoundException($"City not found for Id:{requestModel.CityId}");

        await CheckDistrictExist(requestModel);

        Data.Entities.District createdDistrict = _mapper.Map<Data.Entities.District>(requestModel);
        createdDistrict.CityCode = city.CityCode;
        createdDistrict.CountryCode = city.CountryCode;
        createdDistrict.CountryId = city.CountryId;

        return _mapper.Map<DistrictDto>(await _districtRepository.AddAsync(createdDistrict));
    }

    public async Task<DistrictDto> UpdateAsync(string id, DistrictRequestModel requestModel)
    {
        if (!await _districtRepository.AnyAsync(district => district.Id == id))
            throw new ItemNotFoundException(requestModel.Name);

        if (!await _cityRepository.AnyAsync(city => city.Id == requestModel.CityId))
            throw new NotFoundException($"City not found for Id:{requestModel.CityId}");

        await CheckDistrictExist(requestModel, id);

        var updatedDistrict = await _districtRepository.UpdateAsync(id, _mapper.Map<Data.Entities.District>(requestModel));
        return _mapper.Map<DistrictDto>(updatedDistrict);
    }

    public async Task DeleteAsync(string id)
    {
        if (!await _districtRepository.AnyAsync(district => district.Id == id))
            throw new ItemNotFoundException(id);

        await _districtRepository.DeleteAsync(id);
    }

    private async Task CheckDistrictExist(DistrictRequestModel requestModel, string id = null)
    {
        bool isDistrictExist = await _districtRepository.AnyAsync(district =>
            (string.IsNullOrEmpty(id) || district.Id != id)
            && district.CityId == requestModel.CityId
            && district.Name == requestModel.Name
            && district.IsDeleted == false);

        if (isDistrictExist)
            throw new ItemAlreadyExistsException(requestModel.Name);
    }
}