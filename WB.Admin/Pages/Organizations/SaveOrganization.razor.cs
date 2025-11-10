using Microsoft.AspNetCore.Components;
using Radzen;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;
using WB.Shared.Enums;

namespace WB.Admin.Pages.Organizations
{
    public partial class SaveOrganization : ComponentBase
    {
        #region Parameters
        [Parameter]
        public int? OrganizationTypeId { get; set; }
        [Parameter]
        public string? OrganizationId { get; set; }
        #endregion

        #region Variables Declaration
        private SaveOrganizationRequestDto OrganizationDetail { get; set; } = new();
        private IEnumerable<EnumDropdownItem> OrganizationTypes = new List<EnumDropdownItem>();
        private IEnumerable<EnumDropdownItem> PaymentMethods = new List<EnumDropdownItem>();
        bool IsCashSelected { get; set; }
        bool IsCreditSelected { get; set; }

        #endregion

        #region On Component Load
        protected override async Task OnInitializedAsync()
        {
            PopulateOrganizationTypesDropdown();
            PopulatePaymentMethodsDropdown();
            if (OrganizationId != null)
                await GetOrganizationDetail();
            if (OrganizationId == null && OrganizationTypeId.HasValue)
                OrganizationDetail.TypeId = OrganizationTypeId.Value;
        }
        #endregion

        #region Populate Organization Detail
        private async Task GetOrganizationDetail()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<OrganizationDetailResponseDto>($"Organizations/GetOrganizationDetail?culture=en-Us&organizationId={Guid.Parse(OrganizationId)}");
                if (response.IsSuccessStatusCode)
                {
                    OrganizationDetail = mapper.Map<SaveOrganizationRequestDto>(response.ResultData);
                    IsCashSelected = OrganizationDetail.PaymentMethods.Any(p => p.MethodId == (int)GeneralEnums.PaymentMethodEnum.Cash);
                    IsCreditSelected = OrganizationDetail.PaymentMethods.Any(p => p.MethodId == (int)GeneralEnums.PaymentMethodEnum.Credit);
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    await invalidRequestHandler.ReturnBadRequestNotification(response);
                }
            }
            catch (Exception)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = "Something_went_wrong_Please_try_again",
                });
            }
        }
        #endregion

        #region Populate Dropdowns
        private void PopulateOrganizationTypesDropdown()
        {
            try
            {
                OrganizationTypes = Enum.GetValues(typeof(GeneralEnums.OrganizationTypeEnum))
                    .Cast<GeneralEnums.OrganizationTypeEnum>()
                    .Select(e => new EnumDropdownItem
                    {
                        Id = (int)e,
                        Name = e.GetDisplayName()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }

        private void PopulatePaymentMethodsDropdown()
        {
            try
            {
                PaymentMethods = Enum.GetValues(typeof(GeneralEnums.PaymentMethodEnum))
                    .Cast<GeneralEnums.PaymentMethodEnum>()
                    .Select(e => new EnumDropdownItem
                    {
                        Id = (int)e,
                        Name = e.GetDisplayName()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

        #region On Form Submit
        private async Task OnSubmitSaveOrganization()
        {
            try
            {
                bool? dialogResponse = await dialogService.Confirm(
                translationState.Translate("Sure_Submit"),
                translationState.Translate("Confirm"),
                new ConfirmOptions()
                {
                    CancelButtonText = translationState.Translate("Cancel"),
                    OkButtonText = translationState.Translate("OK")
                });
                if (dialogResponse == true)
                {
                    OrganizationDetail.LoggedInUserId = loginState.UserDetail.UserId;

                    var response = await apiHelper.SendAsync<SaveOrganizationRequestDto, object>(HttpMethod.Post, "Organizations/SaveOrganization", OrganizationDetail, hasReturnData: false);
                    if (response.IsSuccessStatusCode)
                    {
                        notificationService.Notify(new NotificationMessage()
                        {
                            Severity = NotificationSeverity.Success,
                            Detail = OrganizationDetail.Id ==  null ? "Organization Added Successfully" : "Organization Updated Successfully",
                            Duration = 2000
                        });
                        navigationManager.NavigateTo($"/organizations/{OrganizationTypeId}");
                    }
                    else
                    {
                        await invalidRequestHandler.ReturnBadRequestNotification(response);
                    }
                }
            }
            catch (Exception)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

        #region Enum Dropdown Items
        protected class EnumDropdownItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        #endregion

        #region Redirect Functions & Change Events
        void OnPaymentMethodToggled(bool isChecked, int methodId)
        {
            var existing = OrganizationDetail.PaymentMethods.FirstOrDefault(x => x.MethodId == methodId);

            if (isChecked)
            {
                if (existing == null)
                    OrganizationDetail.PaymentMethods.Add(new SaveOrganizationPaymentMethodRequestDto
                    {
                        MethodId = methodId
                    });
            }
            else
            {
                if (existing != null)
                    OrganizationDetail.PaymentMethods.Remove(existing);
            }
        }

        private async Task OnSubmitCancelled()
        {
            bool? dialogResponse = await dialogService.Confirm(
                translationState.Translate("Sure_Cancel"),
                translationState.Translate("Confirm"),
                new ConfirmOptions()
                {
                    CancelButtonText = translationState.Translate("Cancel"),
                    OkButtonText = translationState.Translate("OK")
                });
            if (dialogResponse == true)
            {
                navigationManager.NavigateTo($"/organizations/{OrganizationTypeId}");
            }
        }
        #endregion
    }
}
