using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.Organization.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController(IOrganizationService _iOrganizationService) : ControllerBase
    {
        #region Organization
        [HttpGet("GetOrganizationTypesList")]
        public async Task<IActionResult> GetOrganizationTypesList()
        {
            try
            {
                return Ok(await _iOrganizationService.GetOrganizationTypesList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("UpdateOrganizationType")]
        public async Task<IActionResult> UpdateOrganizationType(UpdateOrganizationTypeRequestDto updateOrganizationTypeRequest)
        {
            try
            {
                await _iOrganizationService.UpdateOrganizationType(updateOrganizationTypeRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetOrganizationTypeDetail")]
        public async Task<IActionResult> GetOrganizationTypeDetail(int typeId)
        {
            try
            {
                return Ok(await _iOrganizationService.GetOrganizationTypeDetail(typeId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpPost("SaveOrganization")]
        public async Task<IActionResult> SaveOrganization(SaveOrganizationRequestDto saveOrganizationRequest)
        {
            try
            {
                await _iOrganizationService.SaveOrganization(saveOrganizationRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetOrganizationsList")]
        public async Task<IActionResult> GetOrganizationsList(int? typeId, string culture)
        {
            try
            {
                return Ok(await _iOrganizationService.GetOrganizationsList(typeId, culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetOrganizationDetail")]
        public async Task<IActionResult> GetOrganizationDetail(string culture, Guid organizationId)
        {
            try
            {
                return Ok(await _iOrganizationService.GetOrganizationDetail(culture, organizationId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Department
        [HttpPost("SaveDepartment")]
        public async Task<IActionResult> SaveDepartment(SaveDepartmentRequestDto saveDepartmentRequest)
        {
            try
            {
                await _iOrganizationService.SaveDepartment(saveDepartmentRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetDepartmentsList")]
        public async Task<IActionResult> GetDepartmentsList(Guid organizationId)
        {
            try
            {
                return Ok(await _iOrganizationService.GetDepartmentsList(organizationId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetDepartmentDetail")]
        public async Task<IActionResult> GetDepartmentDetail(Guid departmentId, string culture)
        {
            try
            {
                return Ok(await _iOrganizationService.GetDepartmentDetail(departmentId, culture));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Designation
        [HttpPost("SaveDesignation")]
        public async Task<IActionResult> SaveDesignation(SaveDesignationRequestDto saveDesignationRequest)
        {
            try
            {
                await _iOrganizationService.SaveDesignation(saveDesignationRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetDesignationsList")]
        public async Task<IActionResult> GetDesignationsList()
        {
            try
            {
                return Ok(await _iOrganizationService.GetDesignationsList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("GetDesignationDetail")]
        public async Task<IActionResult> GetDesignationDetail(Guid designationId)
        {
            try
            {
                return Ok(await _iOrganizationService.GetDesignationDetail(designationId));
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
    }
}
