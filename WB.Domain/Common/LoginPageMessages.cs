namespace WB.Domain.Common
{
    public static class LoginPageMessages
    {
        private static readonly Dictionary<string, Dictionary<string, string>> Messages = new()
    {
        { "EN", new Dictionary<string, string>
            {
                { "UserDoesNotExist", "User does not exist!" },
                { "UserEmailOrPasswordIncorrect", "Username or Password combination is incorrect!" },
                { "UserIsDeactivated", "User account is deactivated!" },
                { "UserIsInactiveOrDeleted", "User is either Inactive or Deleted!" },
                { "UseUsernameInsteadOfEmail", "Use username instead of email" },
                { "UserLockedDueToInvalidAttempts", "Account has been locked due to too many wrong attempts!" }
            }
        },
        { "AR", new Dictionary<string, string>
            {
                { "UserDoesNotExist", "!المستخدم غير موجود" },
                { "UserEmailOrPasswordIncorrect", "!تركيبة المستخدم / كلمة المرور غير صحيحة" },
                { "UserIsDeactivated", "!تم تعطيل حساب المستخدم" },
                { "UserIsInactiveOrDeleted", "!المستخدم إما غير نشط أو محذوف" },
                { "UseUsernameInsteadOfEmail", "اسم المستخدم بدلا من البريد الإلكتروني" },
                { "UserLockedDueToInvalidAttempts", "Account has been locked due to too many wrong attempts!" }
            }
        }
    };

        public static string GetMessage(string key, string culture)
        {

            // Default to "AR" (Arabic) if culture is null or empty
            var normalizedCulture = string.IsNullOrEmpty(culture) || culture == "ar-KW" ? "AR" : "EN";

            return Messages.TryGetValue(normalizedCulture, out var localizedMessages) && localizedMessages.ContainsKey(key)
                ? localizedMessages[key]
                : Messages["AR"].ContainsKey(key) ? Messages["AR"][key] : "Error Occured";
        }
    }
}
