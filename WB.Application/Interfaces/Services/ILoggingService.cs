using WB.Shared.Dtos.General;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface ILoggingService
    {
        Task SaveErrorLogDetails(SaveErrorLogRequestDto errorLogDetails);
        Task CreateProcessLog(SaveProcessLogRequestDto processLogDetails);
        Task<List<ListProcessLogResponseDto>> GetProcessLogs(GridPagination GridPagination);
    }
}
