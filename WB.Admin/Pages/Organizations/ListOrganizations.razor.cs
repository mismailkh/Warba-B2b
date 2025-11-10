using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Admin.Pages.Organizations
{
    public partial class ListOrganizations : ComponentBase
    {
        #region Parameters
        [Parameter]
        public int? OrganizationTypeId { get; set; }
        #endregion

        #region Variables Declaration
        protected RadzenDataGrid<OrganizationsListResponseDto>? OrganizationsGrid = new RadzenDataGrid<OrganizationsListResponseDto>();
        private List<OrganizationsListResponseDto> OrganizationsList = new();
        protected string search { get; set; }
        private string PageTitle;
        #endregion

        #region On Component Load
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            SetPageTitle();
        }
        #endregion

        #region On Load Grid Data
        private async Task LoadData()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<List<OrganizationsListResponseDto>>($"Organizations/GetOrganizationsList?typeId={OrganizationTypeId}&&culture=en-US");
                if (response.IsSuccessStatusCode)
                {
                    OrganizationsList = (List<OrganizationsListResponseDto>)response.ResultData;
                    
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

        private void SetPageTitle()
        {
            if (OrganizationTypeId.HasValue)
            {
                switch (OrganizationTypeId.Value)
                {
                    case 1:
                        PageTitle = translationState.Translate("Internal Organizations");
                        break;
                    case 2:
                        PageTitle = translationState.Translate("External Organizations");
                        break;
                    case 3:
                        PageTitle = translationState.Translate("Partner Organizations");
                        break;
                    default:
                        PageTitle = translationState.Translate("Organizations");
                        break;
                }
            }
            else
            {
                PageTitle = translationState.Translate("Organizations");
            }
        }
    }
}
