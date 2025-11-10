using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml;

namespace WB.Shared.Enums
{
    public class UmsEnums
    {
        public enum IdentityErrorEnum
        {
            ConcurrencyFailure = 1,
            DefaultError = 2,
            DuplicateEmail = 3,
            DuplicateRoleName = 4,
            DuplicateUserName = 5,
            InvalidEmail = 6,
            InvalidRoleName = 7,
            InvalidToken = 8,
            InvalidUserName = 9,
            LoginAlreadyAssociated = 10,
            PasswordMismatch = 11,
            PasswordRequiresDigit = 12,
            PasswordRequiresLower = 13,
            PasswordRequiresNonAlphanumeric = 14,
            PasswordRequiresUniqueChars = 15,
            PasswordRequiresUpper = 16,
            PasswordTooShort = 17,
            RecoveryCodeRedemptionFailed = 18,
            UserAlreadyHasPassword = 19,
            UserAlreadyInRole = 20,
            UserLockoutNotEnabled = 21,
            UserNotInRole = 22
        }
        public enum LoginErrorEnum
        {
            UserLockedDueToInvalidAttempts = 1,
            UserEmailOrPasswordIncorrect = 2,
            UserIsDeactivated = 3,
            UserEmailNotConfirmed = 4,
            UserIsInactiveOrDeleted = 5,
            UserDoesNotExist = 6
        }
        
        public enum GenderEnum
        {
            Male = 1,
            Female = 2
        }
        public enum ContactTypeEnum
        {
            Office = 1,
            Mobile = 2,
            Home = 3
        }
    }
}
