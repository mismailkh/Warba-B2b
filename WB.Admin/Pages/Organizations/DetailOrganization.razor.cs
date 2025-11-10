using Microsoft.AspNetCore.Components;
using Radzen;
using WB.Shared.Dtos.Organization.ResponseDtos;
using WB.Shared.Enums;

namespace WB.Admin.Pages.Organizations
{
    public partial class DetailOrganization : ComponentBase
    {
        #region Parameters
        [Parameter]
        public string OrganizationId { get; set; }
        #endregion

        #region Variables Declaration
        private OrganizationDetailResponseDto OrganizationDetail = new();
        #endregion

        #region On Component Load
        protected override async Task OnInitializedAsync()
        {
            await GetOrganizationTypeDetail();
        }
        #endregion

        #region Populate Organization Type Detail
        private async Task GetOrganizationTypeDetail()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<OrganizationDetailResponseDto>($"Organizations/GetOrganizationDetail?culture=en-US&&organizationId={Guid.Parse(OrganizationId)}");
                if (response.IsSuccessStatusCode)
                {
                    OrganizationDetail = (OrganizationDetailResponseDto)response.ResultData;
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
    }
}