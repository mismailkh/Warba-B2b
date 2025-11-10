using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WB.Web.Helpers
{
    public static class ValidationHelper
    {
        public static bool HasValidationErrors(object accordionDTO)
        {
            var properties = accordionDTO.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var isRequired = prop.GetCustomAttribute<RequiredAttribute>() != null;
                if (!isRequired) continue;

                var value = prop.GetValue(accordionDTO);

                if (value == null ||
                    (value is string str && string.IsNullOrWhiteSpace(str)) ||
                    (value is int i && i == 0) ||
                    (value is double d && d == 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
