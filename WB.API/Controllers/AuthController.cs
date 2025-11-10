using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController(IConfiguration _configuration, IAuthService _iAuthService) : ControllerBase
    {
        #region Login

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var authResponse = await _iAuthService.Login(request.Username, request.Password, request.CultureValue, request.HasTranslations);
                if (!authResponse.Success)
                {
                    //await RecordLoginException(authResponse, null);
                    return BadRequest(new LoginRequestFailedResponse
                    {
                        ErrorCode = authResponse.ErrorCode
                    });
                }
                if (request.ChannelId > 0)
                {
                    UserSucessResponse userSucessResponse = new UserSucessResponse()
                    {
                        Token = authResponse.Token,
                        RefreshToken = authResponse.RefreshToken,
                        UserClaims = authResponse.ClaimsResultList,
                        Translations = authResponse.Translations,
                        User = authResponse.User,
                        UserDetail = authResponse.UserDetail,
                        ProfilePicUrl = authResponse.ProfilePicUrl
                    };
                    return Ok(new ApiCallResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccessStatusCode = true,
                        ResultData = userSucessResponse,
                        Message = "success"
                    });
                }
                return Ok(new UserSucessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken,
                    UserClaims = authResponse.ClaimsResultList,
                    Translations = authResponse.Translations,
                    User = authResponse.User,
                    UserDetail = authResponse.UserDetail,
                    ProfilePicUrl = authResponse.ProfilePicUrl
                });
            }
            catch (Exception ex)
            {
                if (request.ChannelId > 0)
                {
                    return BadRequest(new ApiCallResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccessStatusCode = false,
                        ResultData = new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message },
                        Message = "Error_Ocurred"
                    });
                }
                var response = new BadRequestResponse { Message = ex.Message };
                if (ex.InnerException != null)
                {
                    response.InnerException = ex.InnerException.Message;
                }
                //await RecordLoginException(null, ex.Message);
                return BadRequest(response);
            }
        }

        #endregion

        #region Claim List

        [HttpGet("GetClaimsList")]
        public async Task<IActionResult> GetClaimsList(string culture)
        {
            try
            {
                return Ok(await _iAuthService.GetClaimsList(culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        #endregion

        #region Get User Assigned Claims
        [HttpGet("GetUserAssignedClaims")]
        public async Task<IActionResult> GetUserAssignedClaims(string userId)
        {
            try
            {
                return Ok(await _iAuthService.GetUserAssignedClaims(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        } 
        [HttpGet("GetUserLatestClaimAndRoles")]
        public async Task<IActionResult> GetUserLatestClaimAndRoles(string userId)
        {
            try
            {
                return Ok(await _iAuthService.GetUserLatestClaimAndRoles(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
        #region Get User Security Stamp
        [HttpGet("GetSecurityStampByEmail")]
        public async Task<IActionResult> GetSecurityStampByEmail(string userId)
        {
            try
            {
                return Ok(await _iAuthService.GetSecurityStampByEmail(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region User Password Reset

        [HttpPost("AdminChangeUserPassword")]
        public async Task<IActionResult> AdminChangeUserPassword(AdminChangeUserPasswordRequestDto userPasswordRequest)
        {
            try
            {
                await _iAuthService.AdminChangeUserPassword(userPasswordRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Find User By Email
        [HttpGet("FindUserByEmail")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            try
            {
                var result = await _iAuthService.FindUserByEmail(email);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Find User By Username
        [HttpGet("FindUserByUserName")]
        public async Task<IActionResult> FindUserByUserName(string username)
        {
            try
            {
                var result = await _iAuthService.FindUserByUserName(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
    }
}
