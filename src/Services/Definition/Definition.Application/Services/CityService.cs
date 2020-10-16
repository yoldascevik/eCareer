﻿using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;
using Definition.Application.Services.Interfaces;
using Definition.Data.Entities;

namespace Definition.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<City> _cityRepository;
        private readonly IMongoRepository<Country> _countryRepository;

        public CityService(
            IMongoRepository<City> cityRepository,
            IMongoRepository<Country> countryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<PagedList<CityDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _cityRepository.Get()
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<CityDto> GetByIdAsync(string id)
        {
            var city = await _cityRepository.GetByKeyAsync(id);
            if (city == null)
                throw new ItemNotFoundException(id);
            
            return _mapper.Map<CityDto>(city);
        }

        public async Task<CityDto> CreateAsync(CityRequestModel requestModel)
        {
            Country country = await _countryRepository.FirstOrDefaultAsync(c => c.Id == requestModel.CountryId);
            
            if (country == null)
                throw new NotFoundException($"Country not found for Id:{requestModel.CountryId}");

            await CheckCityExist(requestModel);

            var createdCity = _mapper.Map<City>(requestModel);
            createdCity.CountryCode = country.Iso2; 
            
            return _mapper.Map<CityDto>(await _cityRepository.AddAsync(createdCity));
        }

        public async Task<CityDto> UpdateAsync(string id, CityRequestModel requestModel)
        {
            if (!await _cityRepository.AnyAsync(city => city.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            if (!await _countryRepository.AnyAsync(country => country.Id == requestModel.CountryId))
                throw new NotFoundException($"Country not found for Id:{requestModel.CountryId}");

            await CheckCityExist(requestModel, id);
            
            var updatedCity = await _cityRepository.UpdateAsync(id, _mapper.Map<City>(requestModel));
            return _mapper.Map<CityDto>(updatedCity);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _cityRepository.AnyAsync(city => city.Id == id))
                throw new ItemNotFoundException(id);

            await _cityRepository.DeleteAsync(id);
        }
        
        private async Task CheckCityExist(CityRequestModel requestModel, string id = null)
        {
            var existingCity = await _cityRepository.FirstOrDefaultAsync(city =>
                (string.IsNullOrEmpty(id) || city.Id != id) &&
                city.CountryId == requestModel.CountryId &&
                (city.Name == requestModel.Name || city.CityCode == requestModel.CityCode));

            if (existingCity != null)
            {
                string allreadyExistingData = existingCity.Name == requestModel.Name 
                    ? requestModel.Name 
                    : requestModel.CityCode;
                
                throw new ItemAlreadyExistsException(allreadyExistingData);
            }
        }
    }
}