using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WB.Admin.Pages.Organizations.Dialogs;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Admin.Pages.Organizations
{
    public partial class ListDepartments : ComponentBase
    {
        #region Parameters
        [Parameter]
        public string OrganizationId { get; set; }
        [Parameter]
        public string OrganizationName { get; set; }
        [Parameter]
        public string OrganizationTypeName { get; set; }
        #endregion

        #region Variables Declaration
        protected RadzenDataGrid<DepartmentsListResponseDto>? DepartmentsGrid = new RadzenDataGrid<DepartmentsListResponseDto>();
        private List<DepartmentsListResponseDto> DepartmentsList = new();
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
                var response = await apiHelper.SendGetAsync<List<DepartmentsListResponseDto>>($"Organizations/GetDepartmentsList?organizationId={Guid.Parse(OrganizationId)}");
                if (response.IsSuccessStatusCode)
                {
                    DepartmentsList = (List<DepartmentsListResponseDto>)response.ResultData;
                    
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
        private async Task OpenAddEditDepartmentDialog(Guid? departmentId)
        {
            try
            {
                var result = await dialogService.OpenAsync<AddEditDepartmentDialog>(departmentId == null ? "Add Department" : "Update Department",
                    new Dictionary<string, object>
                    {
                        { "DepartmentId", departmentId.ToString() },
                        { "OrganizationId", OrganizationId },
                        { "OrganizationTypeName", OrganizationTypeName },
                        { "OrganizationName", OrganizationName },
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

        private async Task OpenDetailDepartmentDialog(Guid? departmentId)
        {
            try
            {
                var result = await dialogService.OpenAsync<DetailDepartmentDialog>("View Department",
                    new Dictionary<string, object>
                    {
                        { "DepartmentId", departmentId.ToString() },
                        { "OrganizationTypeName", OrganizationTypeName },
                        { "OrganizationName", OrganizationName },
                    },
                    new DialogOptions() { Width = "40%" });
                if (result != null)
                {
                    var editResult = await dialogService.OpenAsync<AddEditDepartmentDialog>("Update Department",
                    new Dictionary<string, object>
                    {
                        { "DepartmentId", departmentId.ToString() },
                        { "OrganizationId", OrganizationId },
                        { "OrganizationTypeName", OrganizationTypeName },
                        { "OrganizationName", OrganizationName },
                    },
                    new DialogOptions() { Width = "40%" });
                    if (editResult != null)
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

    }
}
