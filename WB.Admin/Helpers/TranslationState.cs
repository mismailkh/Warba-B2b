using Radzen;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;

namespace WB.Admin.Helpers
{
    public class TranslationState
    {
        public string PageSummaryFormat { get { return $"{Translate("Page")} {"{0}"} {Translate("of")} {"{1}"} ({"{2}"} {Translate("items")})"; } }
        public IList<TranslationResponseDto> Translations { get; set; }
        public IList<TranslationResponseDto> FirstLaunchTranslations { get; set; }

        public string Translate(string stringToTranslate)
        {
            try
            {
                if (!string.IsNullOrEmpty(stringToTranslate))
                {
                    var translation = Translations?.Where(x => x.Key.ToLower() == stringToTranslate.ToLower()).FirstOrDefault();
                    if (translation != null)
                    {
                        stringToTranslate = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? translation.ValueEn : translation.ValueAr;
                    }
                }
                return stringToTranslate;
            }
            catch (Exception)
            {
                return stringToTranslate;
            }
        }

        public async Task UpdateTranslation(UpdateTranslationRequestDto updatedTranslation)
        {
            try
            {
                if (!string.IsNullOrEmpty(updatedTranslation.Key))
                {
                    var translation = Translations?.Where(x => x.Key == updatedTranslation.Key).FirstOrDefault();
                    if (translation != null)
                    {
                        var index = Translations.IndexOf(translation);
                        translation.ValueEn = updatedTranslation.ValueEn;
                        translation.ValueAr = updatedTranslation.ValueAr;
                        if (index != -1)
                        {
                            Translations[index] = translation;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
