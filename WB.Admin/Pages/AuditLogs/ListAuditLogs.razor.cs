using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using WB.Shared.Dtos.Organization.ResponseDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Admin.Pages.AuditLogs
{
    public partial class ListAuditLogs : ComponentBase
    {
        #region Parameters
        [Parameter]
        public int? OrganizationTypeId { get; set; }
        #endregion

        #region Variables Declaration
        protected RadzenDataGrid<ListProcessLogResponseDto>? AuditLogsGrid = new RadzenDataGrid<ListProcessLogResponseDto>();
        private List<ListProcessLogResponseDto> AuditLogsList = new();
        protected string search { get; set; }
        private string PageTitle;
        #endregion

        #region On Component Load
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            PageTitle = translationState.Translate("Audit_Logs");
        }
        #endregion

        #region On Load Grid Data
        private async Task LoadData()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<List<ListProcessLogResponseDto>>($"Logging/GetProcessLogs");
                if (response.IsSuccessStatusCode)
                {
                    AuditLogsList = (List<ListProcessLogResponseDto>)response.ResultData;

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
