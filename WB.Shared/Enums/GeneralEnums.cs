using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Enums
{
    public class GeneralEnums
    {
        public enum ModuleEnum
        {
            Ums = 1,
            SiteManagement = 2,
            License = 3,
            Subscriptions = 4,
            PromoCodes = 5,
            Assessments = 6,
            Reporting = 7,
            Sessions = 8,
            Appointment = 9,
            Rooms = 10,
            VRContent = 11,
            Payments = 12,
        }
        public enum OrganizationTypeEnum
        {
            [Display(Name = "Internal")]
            Internal = 1,
            [Display(Name = "External")]
            External = 2,
            [Display(Name = "Partner")]
            Partner = 3,
        }
        public enum PaymentMethodEnum
        {
            [Display(Name = "Cash")]
            Cash = 1,
            [Display(Name = "Credit")]
            Credit = 2,
        }
        public enum LicenseStatusEnum
        {
            Active = 1,
            Inactive = 2,
            Expired = 3
        }
        public enum SubscriptionStatusEnum
        {
            Active = 1,
            Inactive = 2,
            Expired = 3
        }

        public enum LicenseTypeEnum
        {
            [Display(Name = "MHMS")]
            MHMS = 1,
            [Display(Name = "VR")]
            VR = 2,
            [Display(Name = "Both")]
            Both = 3
        }
        public enum SiteDeactivationTypeEnum
        {
            [Display(Name = "Is Permanent Deactivation")]
            IsPermanentDeactivation = 1,
            [Display(Name = "License Base Deactivation")]
            LicenseBaseDeactivation = 2
        }
        public enum ContinentEnum
        {
            Region = 1,
            Country = 2,
            State = 3,
            City = 4
        }
        public enum SiteStatusEnum
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }
        public enum PurchaseRequestStatusEnum
        {
            [Display(Name = "Pending")]
            Pending = 1,
            [Display(Name = "Approved")]
            Approved = 2,
            [Display(Name = "Rejected")]
            Rejected = 3
        }
        public enum RequestTypeEnum
        {
            License = 1,
            Subscription = 2
        }
        public enum PaymentStatusEnum
        {
            [Display(Name = "Pending")]
            Pending = 1,
            [Display(Name = "Paid")]
            Paid = 2,
            [Display(Name = "Failed")]
            Failed = 3
        }
        public enum PaymentMethodTypeEnum
        {
            [Display(Name = "Card")]
            Card = 1,
            [Display(Name = "Bank Transfer")]
            BankTransfer = 2,
            [Display(Name = "Cash")]
            Cash = 3
        }
        public enum PaymentPurposeEnum
        {
            [Display(Name = "License")]
            License = 1,
            [Display(Name = "Subscription")]
            Subscription = 2,
            [Display(Name = "Therapy")]
            Therapy = 3,
            [Display(Name = "Consultation")]
            Consultation = 4
        }
        //for PromoCode
        public enum DiscountTypeEnum
        {
            Percentage = 1,
            FixedAmount = 2
        }
        public enum CodeAssignmentType
        {
            LocationWise = 1,
            Sites = 2
        }
        public enum TermsAndConditionsTypeEnum
        {
            [Display(Name = "Generic")]
            Generic = 1,
            [Display(Name = "Administration_User")]
            Administration_User = 2,
            [Display(Name = "Operational_Admin")]
            Operational_Admin = 3,
            [Display(Name = "MHP")]
            MHP = 4,
            [Display(Name = "Patient")]
            Patient = 5
        }
        public enum VRMaterialTypeEnum 
        {
            [Display(Name = "Video")]
            Video = 1,
            [Display(Name = "Image")]
            Image = 2
        }

        public enum AssessmentStatusEnum
        {
            Active = 1,
            InActive = 2,
            Draft = 3
        }

        public enum QuestionTypeEnum
        {
            SingleChoice = 1,
            MultiChoice = 2,
            FreeText = 3
        } 
    }
}
