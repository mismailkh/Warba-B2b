using static WB.Shared.Enums.NotificationEnums;
using WB.Shared.Enums;

namespace WB.Admin.Extensions
{
    public static class CssClassRenderer
    {
        #region Password Info
        private static (string liClass, string iconClass) RenderPasswordInfoClasses(int infoType, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return ("list-group-item", "mdi mdi-information-variant");
            }
            bool? isValid = infoType switch
            {
                1 => password?.Length >= 6,
                2 => password?.Any(ch => !char.IsLetterOrDigit(ch)),
                3 => password?.Any(char.IsDigit),
                4 => password?.Any(char.IsUpper)
            };

            return isValid == true
                ? ("list-group-item text-success", "mdi mdi-check-bold text-success")
                : ("list-group-item text-danger", "mdi mdi-close-thick text-danger");
        }
        #endregion

        #region Placeholders Icon
        public static string RenderPlaceholderIcon(string placeholderName)
        {
            switch (placeholderName)
            {
                case var placeholder when placeholder == NotificationPlaceholderEnum.RecieverName.GetDisplayName():
                    return "mdi mdi-account-arrow-left widget-icon rounded-circle bg-primary-lighten text-primary";
                case var placeholder when placeholder == NotificationPlaceholderEnum.SenderName.GetDisplayName():
                    return "mdi mdi-account-arrow-right widget-icon rounded-circle bg-info-lighten text-info";
                case var placeholder when placeholder == NotificationPlaceholderEnum.CreatedDate.GetDisplayName():
                    return "mdi mdi-calendar-today widget-icon rounded-circle bg-warning-lighten text-warning";
                case var placeholder when placeholder == NotificationPlaceholderEnum.RoleName.GetDisplayName():
                    return "mdi mdi-shield-account-variant-outline widget-icon rounded-circle bg-success-lighten text-success";
                default:
                    return "mdi mdi-comment-outline widget-icon rounded-circle bg-secondary-lighten text-secondary";
            }

        }
        #endregion

        #region File Manager Stats Icon
        public static string RenderFileManagerStatsIcon(string itemGroupName)
        {
            switch (itemGroupName)
            {
                case "Storage_Pdf":
                    return "file-pdf-box text-danger";
                case "Storage_Docs":
                    return "microsoft-word text-primary";
                case "Storage_Excel":
                    return "microsoft-excel text-success";
                case "Storage_Images":
                    return "image-multiple-outline text-info";
                default:
                    return "file-compare text-warning";
            }
        }
        #endregion

        #region Notification Event Icon
        public static string RenderNotificationIcon(int eventId)
        {
            switch (eventId)
            {
                case (int)NotificationEventEnum.SharePromoCode:
                    return "mdi mdi-file-export-outline";
                case (int)NotificationEventEnum.SharedFolder:
                    return "mdi mdi-folder-upload";
                case (int)NotificationEventEnum.AssignRole:
                    return "mdi mdi-account-cog";
                case (int)NotificationEventEnum.AddProjectMember:
                    return "mdi mdi-account-group";
                default:
                    return "mdi mdi-bell-outline";
            }
        }
        #endregion

        #region Notification Event Background
        public static string RenderNotificationBackground(int eventId)
        {
            switch (eventId)
            {
                case (int)NotificationEventEnum.SharePromoCode:
                    return "notify-icon bg-primary";
                case (int)NotificationEventEnum.SharedFolder:
                    return "notify-icon bg-warning";
                case (int)NotificationEventEnum.AssignRole:
                    return "notify-icon bg-danger";
                case (int)NotificationEventEnum.AddProjectMember:
                    return "notify-icon bg-success";
                default:
                    return "notify-icon bg-info";
            }
        }
        #endregion

        #region Storage Space Bar Color
        public static string SetStorageBarBackgroundColor(double storageUsagePercentage)
        {
            if (storageUsagePercentage >= 90)
                return "bg-danger";
            if (storageUsagePercentage >= 75)
                return "bg-warning";
            return "bg-success";
        }
        #endregion

    }
}
