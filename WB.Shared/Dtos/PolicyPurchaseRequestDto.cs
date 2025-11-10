using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Dtos
{
    public class PolicyPurchaseRequestDto
    {
        public PremiumCalculationDto PremiumCalculation { get; set; } = new();
        public VehicleInformationDto VehicleInformation { get; set; } = new();
        public AddOnInformationDto AddOnInformation { get; set; } = new();
        public bool IsDiscountRequested { get; set; }
        public bool IsTCModificationRequested { get; set; } = false;
        public InsuredPersonInformationDto InsuredPersonInformation { get; set; } = new();
        public InsuredAddressInformationDto InsuredAddressInformation { get; set; } = new();
        public string? Comments { get; set; }
    }

    #region Premium Calculation Section
    public class PremiumCalculationDto
    {
        [Required]
        public int VehicleBrandId { get; set; } = 1;
        [Required]
        public int VehicleModelId { get; set; } = 1;
        [Required]
        public DateTime VehicleYear { get; set; } = new DateTime(2021, 01, 01);
        [Required]
        public int PackageId { get; set; } = 1;
        [Required]
        public int PolicyDuration { get; set; } = 1;
        [Required]
        public DateTime PolicyStartDate { get; set; } = new DateTime(2026, 01, 01);
        public DateTime? PolicyEndDate { get; set; }
        [Required]
        public string FuturePolicyDateReason { get; set; }
        [Required]
        public double SumInsuredFirstYear { get; set; } = 8000;
        [Required]
        public double BasicPremiumFirstYear { get; set; } = 240;
        [Required]
        public double SumInsuredSecondYear { get; set; } = 7000;
        [Required]
        public double BasicPremiumSecondYear { get; set; } = 210;
        [Required]
        public double SumInsuredThirdYear { get; set; } = 6000;
        [Required]
        public double BasicPremiumThirdYear { get; set; } = 180;
        [Required]
        public double TotalSumInsured { get; set; } = 21000;
        [Required]
        public double TotalBasicPremium { get; set; } = 630;
        [Required]
        public bool IsBeneficiarySameAsInsured { get; set; } = true;
        [Required]
        public DateTime DateOfBirth { get; set; } = new DateTime(1995, 12, 25);
        [Required]
        public int Age { get; set; } = 30;
    }
    #endregion

    #region Vehicle Information Section
    public class VehicleInformationDto
    {
        [Required]
        public int VehicleBrandId { get; set; } = 1;
        [Required]
        public int VehicleModelId { get; set; } = 1;
        [Required]
        public string PlateNumber { get; set; } = "125-74-222";
        [Required]
        public string ChasisNumber { get; set; } = "8514-7851222";
        [Required]
        public int VehicleTypeId { get; set; } = 1;
        [Required]
        public int FuelTypeId { get; set; } = 1;
        [Required]
        public string PrimaryColor { get; set; } = "Black";
        public string? SecondaryColor { get; set; }
        [Required]
        public string VehicleShape { get; set; } = "Sedan";
        [Required]
        public int SeatingCapacity { get; set; } = 4;
        [Required]
        public string PurposeOfRegistration { get; set; } = "Lorem ipsum";
        public string? Mileage { get; set; }
        public string? EnginePower { get; set; }
    }
    #endregion

    #region AddOns Section
    public class AddOnInformationDto
    {
        public bool IsRoadSideAssistanceIncluded { get; set; } = true;
        public AddOnRoadsideAssistInformationDto AddOnRoadsideAssistInformation { get; set; } = new();
        public bool IsPaintProtectionIncluded { get; set; } = true;
        public AddOnPaintProtectionInformationDto AddOnPaintProtectionInformation { get; set; } = new();
        public bool IsPersonalAccidentPlanIncluded { get; set; } = true;
        public AddOnPersonalAccidentPlanInformationDto AddOnPersonalAccidentPlanInformation { get; set; } = new();
        public bool IsReplacementVehicleIncluded { get; set; } = true;
        public AddOnReplacementVehicleInformationDto AddOnReplacementVehicleInformation { get; set; } = new();
        public bool IsSubrogationWaiverIncluded { get; set; } = true;
    }
    public class AddOnRoadsideAssistInformationDto
    {
        
        public int RoadSideAssistanceDuration { get; set; } = 1;
        public int RoadAssistanceProviderId { get; set; } = 1;
        public int? RoadAssistanceFee { get; set; } = 21;
    }
    public class AddOnPaintProtectionInformationDto
    {
        
        public int PaintProtectionValue { get; set; }
        public int? PaintProtectionFee { get; set; } = 75;
        public bool IsPaintProtectionInsideAgency { get; set; }
    }
    public class AddOnPersonalAccidentPlanInformationDto
    {
        public int PersonalAccidentPlanId { get; set; } = 1;
        public int? PersonalAccidentFee { get; set; } = 10;
    }
    public class AddOnReplacementVehicleInformationDto
    {
        public int ReplacementVehicleSize { get; set; } = 1;
        public int? ReplacementVehicleFee { get; set; } = 30;
        public int ReplacementVehicleProviderId { get; set; } = 1;
        public int ReplacementVehicleDuration { get; set; } = 1;
        public int ReplacementVehicleDays { get; set; } = 1;
    }
    #endregion

    #region Insured Person Information Section
    public class InsuredPersonInformationDto
    {
        [Required]
        public string InsuredPersonName { get; set; } = "Sara";
        [Required]
        public string BeneficiaryName { get; set; } = "Sara";
        [Required]
        public long CivilId { get; set; } = 7981221414;
        [Required]
        public int JobTitleId { get; set; } = 1;
        [Required]
        public int NationalityId { get; set; } = 1;
        [Required]
        public int GenderId { get; set; } = 1;
        [Required]
        public int MobileNumber { get; set; } = 66077414;
        [Required]
        public int WhatsAppNumber { get; set; } = 66077412;
        public string? TelephoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; } = "SaraS@gmail.com";
    }
    #endregion

    #region Insured Address Information Section
    public class InsuredAddressInformationDto
    {
        [Required]
        public int GovernerateId { get; set; } = 1;
        [Required]
        public int BlockNumber { get; set; } = 3;
        [Required]
        public string GovernerateDescription { get; set; } = "Sharq";
        [Required]
        public int AreaNumber { get; set; } = 12;
        [Required]
        public string StreetName { get; set; } = "123";
        [Required]
        public int HomeNumber { get; set; } = 45;
    }
    #endregion
}
