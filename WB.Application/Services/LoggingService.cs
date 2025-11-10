using AutoMapper;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.General;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Services
{
    public class LoggingService(ILoggingRepository _loggingRepository, IMapper _mapper) : ILoggingService
    {

        public async Task SaveErrorLogDetails(SaveErrorLogRequestDto errorLogDetails)
        {
            try
            {
                await _loggingRepository.SaveErrorLogDetails(errorLogDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateProcessLog(SaveProcessLogRequestDto processLogDetails)
        {
            try
            {
                await _loggingRepository.CreateProcessLog(processLogDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<ListProcessLogResponseDto>> GetProcessLogs(GridPagination GridPagination)
        {
            try
            {
                return _mapper.Map<List<ListProcessLogResponseDto>>(await _loggingRepository.GetProcessLogs(GridPagination));
                //mapped.ForEach(x => x.TotalCount = GridPagination.TotalCount);
                //mapped.First().TotalCount = GridPagination.TotalCount == null ? 0 : GridPagination.TotalCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ListErrorLogResponseDto>> GetErrorLogs(GridPagination GridPagination)
        {
            try
            {
                var mapped = _mapper.Map<List<ListErrorLogResponseDto>>(await _loggingRepository.GetErrorLogs(GridPagination));
                //mapped.ForEach(x => x.TotalCount = GridPagination.TotalCount);
                mapped.First().TotalCount = GridPagination.TotalCount;
                return mapped;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
