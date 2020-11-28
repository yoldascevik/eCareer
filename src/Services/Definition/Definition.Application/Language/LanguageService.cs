using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Career.Data.Pagination;
using Career.Exceptions.Exceptions;
using Career.Mongo.Repository.Contracts;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Data.Entities.Language> _languageRepository;

        public LanguageService(
            IMongoRepository<Data.Entities.Language> languageRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _languageRepository = languageRepository;
        }

        public async Task<PagedList<LanguageDto>> GetAsync(PaginationFilter paginationFilter)
        {
            return await _languageRepository.Get(language => !language.IsDeleted)
                .OrderBy(l => l.Id)
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(paginationFilter);
        }

        public async Task<LanguageDto> GetByIdAsync(string id)
        {
            var language = await _languageRepository.GetByKeyAsync(id);
            if (language == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> GetByCultureAsync(string culture)
        {
            var language = await _languageRepository.FirstOrDefaultAsync(lang =>
                lang.Culture.ToLowerInvariant() == culture.ToLowerInvariant() && !lang.IsDeleted);

            if (language == null)
                throw new ItemNotFoundException(culture);

            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> CreateAsync(LanguageRequestModel requestModel)
        {
            await CheckLanguageExist(requestModel);
            var createdLanguage = await _languageRepository.AddAsync(_mapper.Map<Data.Entities.Language>(requestModel));

            return _mapper.Map<LanguageDto>(createdLanguage);
        }

        public async Task<LanguageDto> UpdateAsync(string id, LanguageRequestModel requestModel)
        {
            if (!await _languageRepository.AnyAsync(language => language.Id == id))
                throw new ItemNotFoundException(requestModel.Name);

            await CheckLanguageExist(requestModel, id);

            var updatedLanguage = await _languageRepository.UpdateAsync(id, _mapper.Map<Data.Entities.Language>(requestModel));
            return _mapper.Map<LanguageDto>(updatedLanguage);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _languageRepository.AnyAsync(language => language.Id == id))
                throw new ItemNotFoundException(id);

            await _languageRepository.DeleteAsync(id);
        }

        private async Task CheckLanguageExist(LanguageRequestModel requestModel, string id = null)
        {
            var existingLanguage = await _languageRepository.FirstOrDefaultAsync(language =>
                (string.IsNullOrEmpty(id) || language.Id != id)
                && language.IsDeleted == false
                && (language.Name == requestModel.Name
                    || language.NativeName == requestModel.NativeName
                    || language.Culture == requestModel.Culture
                    || language.TwoLetterISOLanguageName == requestModel.TwoLetterISOLanguageName
                    || language.ThreeLetterISOLanguageName == requestModel.ThreeLetterISOLanguageName));

            if (existingLanguage != null)
            {
                var allreadyExistingDataList = new List<object>();

                if (existingLanguage.Name == requestModel.Name)
                    allreadyExistingDataList.Add(existingLanguage.Name);

                if (existingLanguage.NativeName == requestModel.NativeName)
                    allreadyExistingDataList.Add(existingLanguage.NativeName);

                if (existingLanguage.Culture == requestModel.Culture)
                    allreadyExistingDataList.Add(existingLanguage.Culture);

                if (existingLanguage.TwoLetterISOLanguageName == requestModel.TwoLetterISOLanguageName)
                    allreadyExistingDataList.Add(existingLanguage.TwoLetterISOLanguageName);

                if (existingLanguage.ThreeLetterISOLanguageName == requestModel.ThreeLetterISOLanguageName)
                    allreadyExistingDataList.Add(existingLanguage.ThreeLetterISOLanguageName);

                throw new ItemAlreadyExistsException(string.Join(",", allreadyExistingDataList));
            }
        }
    }
}