using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILoggingService _iLoggingService;
        public LoggingController(ILoggingService iLoggingService)
        {
            _iLoggingService = iLoggingService;
        }

        [MapToApiVersion("1.1")]
        [HttpPost("SaveErrorLogDetails")]
        public async Task<IActionResult> SaveLogDetails(SaveErrorLogRequestDto errorLogDetails)
        {
            try
            {
                await _iLoggingService.SaveErrorLogDetails(errorLogDetails);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [MapToApiVersion("1.1")]
        [HttpPost("CreateProcessLog")]
        public async Task<IActionResult> CreateProcessLog(SaveProcessLogRequestDto processLogDetails)
        {
            try
            {
                await _iLoggingService.CreateProcessLog(processLogDetails);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [MapToApiVersion("1.1")]
        [HttpGet("GetProcessLogs")]
        public async Task<IActionResult> GetProcessLogs()
        {
            try
            {
                GridPagination GridPagination = null;
               return Ok(await _iLoggingService.GetProcessLogs(GridPagination));
            }   
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
