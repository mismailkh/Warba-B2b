using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Application.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.Product.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController(IConfiguration _configuration, IProductService _iproductService) : ControllerBase
    {
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _iproductService.GetAllProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetProductDetail")]
        public async Task<IActionResult> GetProductDetail(int Id)
        {
            try
            {
                return Ok(await _iproductService.GetProductDetail(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestDto request)
        {
            try
            {
                await _iproductService.UpdateProduct(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
