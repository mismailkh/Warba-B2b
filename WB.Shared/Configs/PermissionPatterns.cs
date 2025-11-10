namespace WB.Shared.Configs
{
    public static class PermissionPatterns
    {
        //String name follows the pattern -> (Module)(Component/Entity) & string value follows the permission pattern without screen type
        // for list page used = List
        // for Add scenario used = Add
        // for Update scenario used = Update
        // for Delete scenario used = Delete
        // for Active/InActive scenario used = IsActive
        // for View Detail scenario used = ViewDetail

        // We will use "Admin" prefix, when adding claim for Admin portal and same like use "Web" prefix for Web Portal while adding claim.

        #region Admin Portal

        //UMS Admin
        public static string UmsAdminUser = "Permissions.Admin.UMS.User.";
        public static string UmsAdminRole = "Permissions.Admin.UMS.Role.";
        public static string UmsAdminLookupsNotification = "Permissions.Admin.UMS.Lookups.Notification.";
        public static string UmsAdminLookupsNotificationEvent = "Permissions.Admin.UMS.Lookups.NotificationEvent.";
        public static string UmsAdminLookupRegion = "Permissions.Admin.UMS.Lookups.Region.";
        public static string UmsAdminLookupCountry = "Permissions.Admin.UMS.Lookups.Country.";
        public static string UmsAdminLookupState = "Permissions.Admin.UMS.Lookups.State.";
        public static string UmsAdminLookupCity = "Permissions.Admin.UMS.Lookups.City.";
        public static string UmsAdminLookupHealthBody = "Permissions.Admin.UMS.Lookups.HealthBody.";
        public static string UmsAdminLookupGender = "Permissions.Admin.UMS.Lookups.Gender.";
        public static string UmsAdminLookupNationality = "Permissions.Admin.UMS.Lookups.Nationality.";
        public static string UmsAdminLookupTranslations = "Permissions.UMS.Lookups.Translation.";

        public static string AdminSiteManagement = "Permissions.Admin.Site.";
        public static string AdminSiteSystemAdmin = "Permissions.Admin.Site.SystemAdmin.";
        public static string AdminVRContent = "Permissions.Admin.VRContent.";
        public static string AdminLicense = "Permissions.Admin.License.";
        public static string AdminSubscription = "Permissions.Admin.Subscription.";
        public static string AdminPromoCode = "Permissions.Admin.PromoCode.";
        public static string AdminTermAndCondition = "Permissions.Admin.TermAndCondition.";

        
        public static Dictionary<string, string> routesAndClaims = new Dictionary<string, string>
        {
            // User Management
            { "/users", PermissionPatterns.UmsAdminUser + "List" },
            { "/roles", PermissionPatterns.UmsAdminRole + "List" },

            // Lookups Management
            { "/Region", PermissionPatterns.UmsAdminLookupRegion + "List" },
            { "/Country", PermissionPatterns.UmsAdminLookupCountry + "List" },
            { "/Nationality", PermissionPatterns.UmsAdminLookupNationality + "List" },
            { "/translations", PermissionPatterns.UmsAdminLookupTranslations + "List" },

            // Site Management
            { "/sites-management", PermissionPatterns.AdminSiteManagement + "List" },

            // VR Content
            { "/vrcontent", PermissionPatterns.AdminVRContent + "List" },

            // License
            { "/list-licenses", PermissionPatterns.AdminLicense + "List" },

            // Subscription
            { "/list-subscriptions", PermissionPatterns.AdminSubscription + "List" },

            // Promo Code
            { "/promos", PermissionPatterns.AdminPromoCode + "List" },

            // Term and Condition
            { "/terms-and-conditions", PermissionPatterns.AdminTermAndCondition + "List" }
        };

        #endregion

        #region Web Portal
        // UMS Web
        public static string UmsWebUser = "Permissions.Web.UMS.User.";
        public static string UmsWebRole = "Permissions.Web.UMS.Role.";
        public static string UmsWebLookupsNotification = "Permissions.Web.UMS.Lookups.Notification.";
        public static string UmsWebLookupsNotificationEvent = "Permissions.Web.UMS.Lookups.NotificationEvent.";
        public static string UmsWebLookupTranslations = "Permissions.Web.Lookups.Translation.";

        public static Dictionary<string, string> routesAndClaimsWeb = new Dictionary<string, string>
        {
            // User Management
            { "/users", PermissionPatterns.UmsWebUser + "List" },
            { "/roles", PermissionPatterns.UmsWebRole + "List" },

            // Lookups Management
            { "/translations", PermissionPatterns.UmsWebLookupTranslations + "List" }
        };
        #endregion
    }
}
