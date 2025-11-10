using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WB.Admin.Pages.Organizations.Dialogs;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Admin.Pages.Organizations
{
    public partial class ListOrganizationTypes : ComponentBase
    {
        #region Variables Declaration
        protected RadzenDataGrid<OrganizationTypesListResponseDto>? OrganizationTypesGrid = new RadzenDataGrid<OrganizationTypesListResponseDto>();
        private List<OrganizationTypesListResponseDto> OrganizationTypesList = new();
        private List<OrganizationTypesListResponseDto> FilteredOrganizationTypesList = new();
        protected string search { get; set; }
        #endregion

        #region On Component Load
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        #endregion

        #region On Load Grid Data
        private async Task LoadData()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<List<OrganizationTypesListResponseDto>>($"Organizations/GetOrganizationTypesList");
                if (response.IsSuccessStatusCode)
                {
                    FilteredOrganizationTypesList = OrganizationTypesList = (List<OrganizationTypesListResponseDto>)response.ResultData;
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
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

        #region Button Events
        private async Task OpenEditOrganizationTypeDialog(int typeId)
        {
            try
            {
                var result = await dialogService.OpenAsync<EditOrganizationTypeDialog>(translationState.Translate("Update_Organization_Type"),
                    new Dictionary<string, object> {
                    { "OrganizationTypeId", typeId }
                    },
                    new DialogOptions() { Width = "40%" });
                if (result != null)
                {
                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }

        private async Task OpenDetailOrganizationTypeDialog(int typeId)
        {
            try
            {
                var result = await dialogService.OpenAsync<DetailOrganizationTypeDialog>(translationState.Translate("View_Organization_Type"),
                    new Dictionary<string, object> {
                    { "OrganizationTypeId", typeId }
                    },
                    new DialogOptions() { Width = "40%" });
                if (result != null)
                {
                    var editResult = await dialogService.OpenAsync<EditOrganizationTypeDialog>(translationState.Translate("Update_Organization_Type"),
                    new Dictionary<string, object> {
                    { "OrganizationTypeId", typeId }
                    },
                    new DialogOptions() { Width = "40%" });
                    if (result != null)
                    {
                        await LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

        #region Grid Search
        protected async Task OnSearchInput()
        {
            try
            {
                search = string.IsNullOrEmpty(search) ? "" : search.TrimStart().TrimEnd().ToLower();
                FilteredOrganizationTypesList = await gridSearchExtension.Filter(OrganizationTypesList, new Query()
                {
                    Filter = $@"i => (i.NameEn != null && i.NameEn.ToLower().Contains(@0)) || (i.NameAr != null && i.NameAr.ToLower().Contains(@0))",
                    FilterParameters = new object[] { search }
                });
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion
    }
}
