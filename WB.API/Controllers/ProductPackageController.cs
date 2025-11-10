using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Application.Services;
using WB.Shared.Dtos.CarMake.Request;
using WB.Shared.Dtos.ProductPackage.Request;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPackageController(IProductPackageService _iProductPackageService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _iProductPackageService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductPackageRequestDto createProductPackageRequestDto)
        {
            try
            {
                await _iProductPackageService.Create(createProductPackageRequestDto);
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
                var result = await _iProductPackageService.GetById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetCarMakes")]
        public async Task<IActionResult> GetCarMakes(Guid ProductPackageId)
        {
            try
            {
                var result = await _iProductPackageService.GetCarMakes(ProductPackageId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
