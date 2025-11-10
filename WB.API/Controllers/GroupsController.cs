using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupsController(IGroupService _iGroupService) : ControllerBase
    {
        [HttpGet("GetGroupsList")]
        public async Task<IActionResult> GetGroupsList(string culture)
        {
            try
            {
                return Ok(await _iGroupService.GetGroupsList(culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpGet("GetGroupDetail")]
        public async Task<IActionResult> GetGroupDetail(string culture, Guid groupId)
        {
            try
            {
                return Ok(await _iGroupService.GetGroupDetail(culture, groupId));

            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpPost("SaveGroup")]
        public async Task<IActionResult> SaveGroup(SaveGroupRequestDto groupRequest)
        {
            try
            {
                await _iGroupService.SaveGroup(groupRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        [HttpPost("SaveGroupAccess")]
        public async Task<IActionResult> SaveGroupAccess(SaveGroupAccessRequestDto groupAccessRequest)
        {
            try
            {
                await _iGroupService.SaveGroupAccess(groupAccessRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
