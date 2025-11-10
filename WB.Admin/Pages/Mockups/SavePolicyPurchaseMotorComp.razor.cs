using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using Radzen;
using Soenneker.Blazor.FilePond;
using Soenneker.Blazor.FilePond.Enums;
using Soenneker.Blazor.FilePond.Options;
using WB.Shared.Dtos;

namespace WB.Admin.Pages.Mockups
{
    public partial class SavePolicyPurchaseMotorComp : ComponentBase
    {
        #region Variables Declaration
        private PolicyPurchaseRequestDto PolicyPurchaseRequest = new PolicyPurchaseRequestDto();
        private bool IsDisabled = true;
        private bool CheckboxTrue = true;
        private bool CheckboxFalse = false;
        private bool ShowNumericUpDown = false;
        private bool isRendered = false;
        BlazoredTextEditor? QuillHtml;
        private FilePond? FilePond { get; set; }
        #endregion

        #region On Component Load
        protected override void OnInitialized()
        {
            PolicyPurchaseRequest.PremiumCalculation.PolicyEndDate = PolicyPurchaseRequest.PremiumCalculation.PolicyStartDate.AddYears(PolicyPurchaseRequest.PremiumCalculation.PolicyDuration).AddDays(-1);
        }
        #endregion

        #region On Component Rendered
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isRendered = true;
                await Task.Delay(0);
                StateHasChanged();
            }
        }
        #endregion

        #region Dummy Data
        private int SelectedValue = 1;
        private string SampleText = "Lorem ipsum dolor sit amet consectetur adipisicing elit.";

        private IEnumerable<object> VehicleBrands = new List<object>
        {
            new { Id = 1, Name = "Toyota" },
            new { Id = 2, Name = "Honda" },
            new { Id = 3, Name = "Suzuki" },
            new { Id = 4, Name = "Kia" },
            new { Id = 5, Name = "Hyundai" }
        };

        private IEnumerable<object> VehicleModels = new List<object>
        {
            new { Id = 1, Name = "Camry" },
            new { Id = 2, Name = "Hilux" }
        };

        private IEnumerable<object> Packages = new List<object>
        {
            new { Id = 1, Name = "Gold" },
            new { Id = 2, Name = "Platinum" }
        };

        private IEnumerable<object> Years = new List<object>
        {
            new { Id = 1, Name = "1 Year" },
            new { Id = 2, Name = "2 Years" },
            new { Id = 3, Name = "3 Years" }
        };

        private IEnumerable<object> Acknowledgements = new List<object>
        {
            new { Id = true, Name = "Yes" },
            new { Id = false, Name = "No" }
        };

        private IEnumerable<object> VehicleTypes = new List<object>
        {
            new { Id = 1, Name = "New" },
            new { Id = 2, Name = "Used" }
        };

        private IEnumerable<object> FuelTypes = new List<object>
        {
            new { Id = 1, Name = "Petrol" },
            new { Id = 2, Name = "Diesel" },
            new { Id = 3, Name = "Hybrid" }
        };

        private IEnumerable<object> Providers = new List<object>
        {
            new { Id = 1, Name = "Select" }
        };

        private IEnumerable<object> AccidentPlans = new List<object>
        {
            new { Id = 1, Name = "5000" },
            new { Id = 2, Name = "10000" }
        };

        private IEnumerable<object> Sizes = new List<object>
        {
            new { Id = 1, Name = "Small" },
            new { Id = 2, Name = "Medium" },
            new { Id = 3, Name = "Large" }
        };

        private IEnumerable<object> Days = new List<object>
        {
            new { Id = 1, Name = "15" },
            new { Id = 2, Name = "30" },
            new { Id = 3, Name = "45" }
        };

        private IEnumerable<object> JobTitles = new List<object>
        {
            new { Id = 1, Name = "Manager" },
            new { Id = 2, Name = "Supervisor" },
            new { Id = 3, Name = "Admin" }
        };
        private IEnumerable<object> PaymentMethods = new List<object>
        {
            new { Id = 1, Name = "Cash" },
            new { Id = 2, Name = "Credit" },
        };
        private IEnumerable<object> Governorates = new List<object>
        {
            new { Id = 1, Name = "Asimah" },
            new { Id = 2, Name = "Farwaniya" },
            new { Id = 3, Name = "Ahmadi" }
        };

        #region Premium Details Data
        public class PremiumDetail
        {
            public string FeeType { get; set; }
            public string Percentage { get; set; }
            public string Amount { get; set; }
            public bool IsSummaryRow { get; set; } = false;
            public bool IsDiscountRow { get; set; } = false;
        }
        public List<PremiumDetail> PremiumDetailsData { get; set; } = new List<PremiumDetail>
    {
        new PremiumDetail { FeeType = "Basic Premium 1st Year", Percentage = "", Amount = "240.000" },
        new PremiumDetail { FeeType = "Basic Premium 2nd Year", Percentage = "", Amount = "210.000" },
        new PremiumDetail { FeeType = "Basic Premium 3rd Year", Percentage = "", Amount = "180.000" },
        new PremiumDetail { FeeType = "Total Basic Premium", Percentage = "", Amount = "630.000", IsSummaryRow = true },
        new PremiumDetail { FeeType = "Promotional Discount", Percentage = "10", Amount = "63.000", IsDiscountRow = true },
        new PremiumDetail { FeeType = "Total Basic Premium after Promotional Discount", Percentage = "", Amount = "567.000", IsSummaryRow = true },
        new PremiumDetail { FeeType = "Roadside Assist", Percentage = "", Amount = "21.000" },
        new PremiumDetail { FeeType = "Paint Protection", Percentage = "", Amount = "75.000" },
        new PremiumDetail { FeeType = "Replacement Vehicle", Percentage = "", Amount = "45.000" },
        new PremiumDetail { FeeType = "Personal Accident Plan", Percentage = "", Amount = "10.000" },
        new PremiumDetail { FeeType = "Waiver of Subrogation", Percentage = "", Amount = "49.5" },
        new PremiumDetail { FeeType = "Beneficiary Fee", Percentage = "", Amount = "1.500" },
        new PremiumDetail { FeeType = "Issue Fee", Percentage = "", Amount = "2.000" },
        new PremiumDetail { FeeType = "Supervision Fee", Percentage = "", Amount = "3.000" },
        new PremiumDetail { FeeType = "Total Gross Premium", Percentage = "", Amount = "774.000", IsSummaryRow = true },
    };
        #endregion

        #endregion

        #region File Upload
        private readonly FilePondOptions _Attachmentsoptions = new()
        {
            MaxFiles = 20,
            AllowMultiple = true,
            EnabledPlugins = [FilePondPluginType.ImagePreview, FilePondPluginType.MediaPreview],
        };
        #endregion

        #region Change Events
        private async Task OpenSubmitDialog()
        {
            await dialogService.Confirm("Are you sure you would like to submit the request for Quotation Request # 87452?",
                "Confirm",
                new ConfirmOptions() { OkButtonText = "Submit", CancelButtonText = "Cancel" });
        }
        private async Task OpenSaveAsDraftDialog()
        {
            await dialogService.Confirm("Are you sure you would like to save this request Quotation Request # 87452 as draft?",
                "Confirm",
                new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "Cancel" });
        }
        #endregion

        #region Business Rules Calculations
        private void CalculatePolicyEndDate()
        {
            PolicyPurchaseRequest.PremiumCalculation.PolicyEndDate = PolicyPurchaseRequest.PremiumCalculation.PolicyStartDate.AddYears(PolicyPurchaseRequest.PremiumCalculation.PolicyDuration).AddDays(-1);
        }
        #endregion
    }
}