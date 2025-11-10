using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.CarMake.Request;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMakeController(ICarMakeService _iCarMakeService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _iCarMakeService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCarMakeRequestDto createCarMakeRequestDto)
        {
            try
            {
                await _iCarMakeService.Create(createCarMakeRequestDto);
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
                var result = await _iCarMakeService.GetById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        //[HttpPut("Update")]
        //public async Task<IActionResult> Update(UpdateCarMakeRequestDto updateCarMakeRequestDto)
        //{
        //    try
        //    {
        //        await _iCarMakeService.Update(updateCarMakeRequestDto);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
        //    }
        //}

        [HttpPost("AddModel")]
        public async Task<IActionResult> AddModel(AddCarModelRequestDto model)
        {
            try
            {
                await _iCarMakeService.AddModel(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetAllModels")]
        public async Task<IActionResult> GetAllModels(Guid CarMakeId)
        {
            try
            {
                var result = await _iCarMakeService.GetAllModels(CarMakeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
    }
}
