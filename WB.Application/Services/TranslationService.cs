using AutoMapper;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Domain.Entities.Lookups;
using WB.Domain.Entities.Ums;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;

namespace WB.Application.Services
{
    public class TranslationService(IGenericRepository<Translation> _genericRepository, IGenericRepository<Language> _languageRepository, IGenericRepository<User> _userRepository, IMapper _mapper) : ITranslationService
    {
        public async Task<TranslationUserCheckResponseDto> GetTranslationsListAndUserCheck()
        {
            try
            {
                TranslationUserCheckResponseDto response = new TranslationUserCheckResponseDto();
                response.Translations = _mapper.Map<List<TranslationResponseDto>>(await _genericRepository.Get());
                response.UserCount = await _userRepository.Count();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<TranslationResponseDto>> GetTranslationsList()
        {
            try
            {
                return _mapper.Map<List<TranslationResponseDto>>(await _genericRepository.Get());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<LanguageListResponseDto>> GetLanguagesList()
        {
            try
            {
                return _mapper.Map<List<LanguageListResponseDto>>(await _languageRepository.Get());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateTranslation(UpdateTranslationRequestDto translation)
        {
            try
            {
                var result = (await _genericRepository.Get(x => x.Key == translation.Key)).FirstOrDefault();
                _mapper.Map(translation, result);
                _genericRepository.Update(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
