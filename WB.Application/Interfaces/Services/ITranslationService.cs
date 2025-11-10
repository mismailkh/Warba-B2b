using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface ITranslationService
    {
        Task<TranslationUserCheckResponseDto> GetTranslationsListAndUserCheck();
        Task<List<TranslationResponseDto>> GetTranslationsList();
        Task<List<LanguageListResponseDto>> GetLanguagesList();
        Task UpdateTranslation(UpdateTranslationRequestDto translation);
    }
}
