using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Enums
{
    public class NotificationEnums
    {

        public enum NotificationChannelEnum
        {
            [Display(Name = "Browser")]
            Browser = 1,
            [Display(Name = "Email")]
            Email = 2,
            [Display(Name = "Mobile")]
            Mobile = 4,
        }
        public enum NotificationStatusEnum
        {
            [Display(Name = "Read")]
            Read = 1,
            [Display(Name = "Unread")]
            Unread = 2,
            [Display(Name = "Sent")]
            Sent = 4,
            [Display(Name = "Received")]
            Received = 8
        }

        public enum NotificationPlaceholderEnum
        {
            [Display(Name = "#Receiver Name#")]
            RecieverName = 1,
            [Display(Name = "#Created Date#")]
            CreatedDate = 2,
            [Display(Name = "#Sender Name#")]
            SenderName = 3,
            [Display(Name = "#Entity#")]
            Entity = 4,
            [Display(Name = "#Role Name#")]
            RoleName = 5,
            [Display(Name = "#Status#")]
            Status = 6
        }

        public enum NotificationEventEnum
        {
            SharePromoCode = 1,
            SharedFolder = 2,
            AddProjectMember = 3,
            AssignRole = 4,
            AddSiteUser = 5,
            ActionAgainstLicensePurchaseRequest = 6,
            CreateNewLicensePurchaseRequest = 7,
            UpdateLicensePurchaseRequest = 8,
            CreateNewSubscriptionPurchaseRequest = 9,
            ActionAgainstSubscriptionPurchaseRequest = 10,
            SiteDeactivation = 11,
            SiteActivation = 12
        }
    }
}
