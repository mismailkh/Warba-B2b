using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LookupsController(IConfiguration _configuration, ILookupService _iLookupService) : ControllerBase
    {
        [HttpPost("GetLookupsData")]
        public async Task<IActionResult> GetLookupsData(LookupRequestDto request)
        {
            try
            {
                return Ok(await _iLookupService.GetLookupsData(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpGet("GetLookupList")]
        public async Task<IActionResult> GetLookupList(string currentLookupType, int parentId)
        {
            try
            {
                return Ok(await _iLookupService.GetLookupList(currentLookupType, parentId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpPost("SaveLookup")]
        public async Task<IActionResult> SaveLookup(SaveLookupRequestDto lookupRequest, string currentLookupType)
        {
            try
            {
                await _iLookupService.SaveLookup(lookupRequest, currentLookupType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpGet("GetLookupDetail")]
        public async Task<IActionResult> GetLookupDetail(int Id, string currentlookupType)
        {
            try
            {
                return Ok(await _iLookupService.GetLookupDetail(Id, currentlookupType));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
