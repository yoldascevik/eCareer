using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Location.Country;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<Data.Entities.Country> _countryRepository;

    public CountryService(IMongoRepository<Data.Entities.Country> countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CountryDto>> GetAsync(PaginationFilter paginationFilter)
    {
        return await _countryRepository.Get(country => !country.IsDeleted)
            .OrderBy(c => c.Id)
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(paginationFilter);
    }

    public async Task<CountryDto> GetByIdAsync(string id)
    {
        var country = await _countryRepository.GetByKeyAsync(id);
        if (country == null)
            throw new ItemNotFoundException(id);

        return _mapper.Map<CountryDto>(country);
    }

    public async Task<CountryDto> GetByCodeAsync(string code)
    {
        var country = await _countryRepository.FirstOrDefaultAsync(c => c.Iso2.ToLowerInvariant() == code.ToLowerInvariant());
        if (country == null)
            throw new ItemNotFoundException(code);

        return _mapper.Map<CountryDto>(country);
    }

    public async Task<CountryDto> CreateAsync(CountryRequestModel requestModel)
    {
        await CheckCountryExist(requestModel);

        Data.Entities.Country createdCountry = await _countryRepository.AddAsync(_mapper.Map<Data.Entities.Country>(requestModel));
        return _mapper.Map<CountryDto>(createdCountry);
    }

    public async Task<CountryDto> UpdateAsync(string id, CountryRequestModel requestModel)
    {
        if (!await _countryRepository.AnyAsync(country => country.Id == id))
            throw new ItemNotFoundException(id);

        await CheckCountryExist(requestModel, id);

        var updatedCountry = await _countryRepository.UpdateAsync(id, _mapper.Map<Data.Entities.Country>(requestModel));
        return _mapper.Map<CountryDto>(updatedCountry);
    }

    public async Task DeleteAsync(string id)
    {
        if (!await _countryRepository.AnyAsync(country => country.Id == id))
            throw new ItemNotFoundException(id);

        await _countryRepository.DeleteAsync(id);
    }

    private async Task CheckCountryExist(CountryRequestModel requestModel, string id = null)
    {
        var existingCountry = await _countryRepository.FirstOrDefaultAsync(country =>
            (string.IsNullOrEmpty(id) || country.Id != id)
            && country.IsDeleted == false
            && (country.Name == requestModel.Name
                || country.Iso2 == requestModel.Iso2
                || country.Iso3 == requestModel.Iso3
                || country.PhoneCode == requestModel.PhoneCode
            ));

        if (existingCountry != null)
        {
            var allreadyExistingDataList = new List<object>();

            if (existingCountry.Name == requestModel.Name)
                allreadyExistingDataList.Add(existingCountry.Name);

            if (existingCountry.Iso2 == requestModel.Iso2)
                allreadyExistingDataList.Add(existingCountry.Iso2);

            if (existingCountry.Iso3 == requestModel.Iso3)
                allreadyExistingDataList.Add(existingCountry.Iso3);

            if (existingCountry.PhoneCode == requestModel.PhoneCode)
                allreadyExistingDataList.Add(existingCountry.PhoneCode);

            throw new ItemAlreadyExistsException(string.Join(",", allreadyExistingDataList));
        }
    }
}