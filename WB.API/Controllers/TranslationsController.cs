using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",WBApiKey")]
    public class TranslationsController(IConfiguration _configuration, ITranslationService _iTranslationService) : ControllerBase
    {
        [HttpGet("GetTranslationsListAndUserCheck")]
        public async Task<IActionResult> GetTranslationsListAndUserCheck()
        {
            try
            {
                return Ok(await _iTranslationService.GetTranslationsListAndUserCheck());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetLanguagesList")]
        public async Task<IActionResult> GetLanguagesList()
        {
            try
            {
                return Ok(await _iTranslationService.GetLanguagesList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetTranslationsList")]
        public async Task<IActionResult> GetTranslationsList()
        {
            try
            {
                return Ok(await _iTranslationService.GetTranslationsList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        #region Edit Event
        [MapToApiVersion("1.1")]
        [HttpPost("UpdateTranslation")]
        public async Task<IActionResult> UpdateTranslation(UpdateTranslationRequestDto translation)
        {
            try
            {
                await _iTranslationService.UpdateTranslation(translation);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
    }
}
