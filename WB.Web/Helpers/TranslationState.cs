using Radzen;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;

namespace WB.Web.Helpers
{
    public class TranslationState
    {
        public string PageSummaryFormat { get; set; }
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

        public void TranslateGridFilterLabels(dynamic grid)
        {
            if (grid != null)
            {
                grid.ClearFilterText = Translate("Clear");
                grid.ApplyFilterText = Translate("Filter");
                grid.IsNotEmptyText = Translate("Is_Not_Empty_Text");
                grid.IsEmptyText = Translate("Is_Empty_Text");
                grid.NotEqualsText = Translate("Not_equals");
                grid.EqualsText = Translate("Equals");
                grid.LessThanText = Translate("less_than");
                grid.LessThanOrEqualsText = Translate("Less_than_or_equals");
                grid.GreaterThanText = Translate("Greater_than");
                grid.GreaterThanOrEqualsText = Translate("Greater_than_or_equals");
                grid.EndsWithText = Translate("Ends_with");
                grid.ContainsText = Translate("Contains");
                grid.DoesNotContainText = Translate("Does_not_contain");
                grid.StartsWithText = Translate("Starts_with");
                grid.IsNullText = Translate("Is_null");
                grid.IsNotNullText = Translate("Is_not_null");
                grid.AndOperatorText = Translate("And");
                grid.OrOperatorText = Translate("Or");
                grid.EmptyText = Translate("Empty");
                grid.FilterText = Translate("Filter");
            }
            PageSummaryFormat = $"{Translate("Page")} {"{0}"} {Translate("of")} {"{1}"} ({"{2}"} {Translate("items")})";
        }

        public async Task UpdateTranslation(UpdateTranslationRequestDto updatedTranslation)
        {
            try
            {
                if(!string.IsNullOrEmpty(updatedTranslation.Key))
                {
                    var translation= Translations?.Where(x => x.Key == updatedTranslation.Key).FirstOrDefault();
                    if (translation != null)
                    {
                        var index = Translations.IndexOf(translation);
                        translation.ValueEn = updatedTranslation.ValueEn;
                        translation.ValueAr = updatedTranslation.ValueAr;
                        if (index!= -1)
                        {
                            Translations[index] = translation;
                        }
                    }
                }
            }
            catch(Exception) {

            }
        }
    }
}
