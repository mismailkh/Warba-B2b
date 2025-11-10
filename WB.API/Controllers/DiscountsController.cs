using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Application.Services;
using WB.Shared.Dtos.Discount.Request;
using WB.Shared.Dtos.ProductPackage.Request;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController(IDiscountService _iDiscountService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _iDiscountService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateDiscountRequestDto createDiscountRequestDto)
        {
            try
            {
                await _iDiscountService.Create(createDiscountRequestDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var result = await _iDiscountService.GetById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Guid DiscountId, AddProductRequestDto addProductRequestDto)
        {
            try
            {
                await _iDiscountService.AddProduct(DiscountId, addProductRequestDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(Guid DiscountId)
        {
            try
            {
                var result = await _iDiscountService.GetProducts(DiscountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
