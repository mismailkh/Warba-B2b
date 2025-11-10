namespace WB.Admin.Helpers
{

    public static class RegexPatterns
    {
        /// <summary>
        ///  Allow English and Arabic along with these Special Characters: (, ), [, ], /, \, ", ', :, & and comma
        /// </summary>
        public const string EmailPattern = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
        public const string MobileNoPattern = "[4,5,6,9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]";
        public const string FingerprintPattern = "[0-6]";
        public const string EnglishPattern = @"^(?=.*[A-Za-z])[A-Za-z0-9()[\]/\\' &:.?#]+( [A-Za-z0-9()[\]/\\'&:.?#]+)*$";
        public const string ArabicPattern = @"^(?=.*[\u0621-\u064A])[\u0621-\u064A0-9\u0660-\u0669\u06F0-\u06F9()[\]/\\' &:.?#]+( [\u0621-\u064A0-9\u0660-\u0669\u06F0-\u06F9()[\]/\\'&:.?#]+)*$";
        public const string EnglishArabicPattern = @"^(?=.*)[A-Za-z\u0621-\u064A0-9\u0660-\u0669\u06F0-\u06F9\s(),[\]/\\\""':&#\.?]+(?=.*)$";
        // The below pattern allow spcial characters, English, Arabic, along with only two digits
        public const string SpecialPattern = @"^(?=.{1,10}$)(?!.*\s{2})[A-Za-z\u0621-\u064A(),[\]/\\\""':&]*(\d{0,3})[A-Za-z\u0621-\u064A(),[\]/\\\""':& ]*$";
        public const string NoLeadingWhiteSpacesPattern = @"^\S(.*\S)?$";
        public const string NoLeadingSpacesPattern = @"^(?:(?!^\s+$).*(?:\r?\n|$))*$";
        //For text area Generic Regex pattern on remarks and Description.
        public const string NoLeadingSpacesTextAreaPattern = @"^(?:(?!^\s+$).*(?:\r?\n|$))*$";
        public const string RestrictFromOnlySpaces = @"^(?!\s*$).+";
        //Password pattern for One Upper, 1 lower, 1 Special and Min 6 character
        public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d])[\s\S]{6,}$";
        // The below pattern allow English characters and Digits 
        public const string EnglishDigitsPattern = "^[A-Za-z0-9]+$";



    }
}
