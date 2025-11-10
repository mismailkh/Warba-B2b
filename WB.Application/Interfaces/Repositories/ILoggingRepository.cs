using WB.Domain.Entities.AuditLogs;
using WB.Shared.Dtos.General;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Repositories
{
    public interface ILoggingRepository
    {
        Task SaveErrorLogDetails(SaveErrorLogRequestDto errorLogDetails);
        Task CreateProcessLog(SaveProcessLogRequestDto processLogDetails);
        Task<List<ListProcessLogResponseDto>> GetProcessLogs(GridPagination GridPagination);
        Task<List<ErrorLog>> GetErrorLogs(GridPagination GridPagination);
    }
}
