using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _iRoleService) : ControllerBase
    {
        [HttpGet("GetRolesList")]
        public async Task<IActionResult> GetRolesList(string culture)
        {
            try
            {
                return Ok(await _iRoleService.GetRolesList(culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetRoleDetails")]
        public async Task<IActionResult> GetRoleDetails(string culture, string roleId)
        {
            try
            {
                return Ok(await _iRoleService.GetRoleDetails(culture, roleId));

            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("SaveRole")]
        public async Task<IActionResult> SaveRole(SaveRoleRequestDto roleRequest)
        {
            try
            {
                await _iRoleService.SaveRole(roleRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("UpdateRoleStatus")]
        public async Task<IActionResult> UpdateRoleStatus(UpdateRoleStatusRequestDto updateRoleStatusRequest)
        {
            try
            {
                await _iRoleService.UpdateRoleStatus(updateRoleStatusRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("SaveUserRole")]
        public async Task<IActionResult> SaveUserRole(SaveRoleAssignmentRequestDto roleAssignmentRequest)
        {
            try
            {
                await _iRoleService.SaveUserRole(roleAssignmentRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            try
            {
                return Ok(await _iRoleService.GetUserRoles(userId));

            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

    }
}
