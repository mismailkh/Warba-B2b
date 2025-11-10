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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",WBApiKey")]
    public class UsersController(IUserService _iUserService) : ControllerBase
    {
        [HttpGet("GetUsersList")]
        public async Task<IActionResult> GetUsersList(string culture)
        {
            try
            {
                return Ok(await _iUserService.GetUsersList(culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(string culture, string userId)
        {
            try
            {
                return Ok(await _iUserService.GetUserDetail(culture, userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetProcessLogsByUserId")]
        public async Task<IActionResult> GetProcessLogsByUserId(string userId)
        {
            try
            {
                return Ok(await _iUserService.GetProcessLogsByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("CreateAdminUser")]
        public async Task<IActionResult> CreateAdminUser(CreateAdminRequestDto adminRequest)
        {
            try
            {
                await _iUserService.CreateAdminUser(adminRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser(SaveUserRequestDto userRequest)
        {
            try
            {
                await _iUserService.SaveUser(userRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("SaveUserAccess")]
        public async Task<IActionResult> SaveUserAccess(SaveUserAccessRequestDto userAccessRequest)
        {
            try
            {
                await _iUserService.SaveUserAccess(userAccessRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus(UpdateUserStatusRequestDto userStatusRequest)
        {
            try
            {
                await _iUserService.UpdateUserStatus(userStatusRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
