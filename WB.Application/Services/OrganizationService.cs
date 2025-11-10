using AutoMapper;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;
using static WB.Shared.Enums.GeneralEnums;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace WB.Application.Services
{
    public class OrganizationService(IOrganizationRepository _organizationRepository, IMapper _mapper, ILoggingService _iLoggingService, IHttpContextAccessor _httpContext) : IOrganizationService
    {
        #region Organization Type
        public async Task<List<OrganizationTypesListResponseDto>> GetOrganizationTypesList()
        {
            try
            {
                var organizationTypes = await _organizationRepository.GetOrganizationTypesList();
                return _mapper.Map<List<OrganizationTypesListResponseDto>>(organizationTypes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateOrganizationType(UpdateOrganizationTypeRequestDto updateOrganizationTypeRequest)
        {
            try
            {
                await _organizationRepository.UpdateOrganizationType(updateOrganizationTypeRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Organization
        public async Task<List<OrganizationsListResponseDto>> GetOrganizationsList(int? typeId, string culture)
        {
            try
            {
                return await _organizationRepository.GetOrganizationsList(typeId, culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<OrganizationTypeDetailResponseDto> GetOrganizationTypeDetail(int typeId)
        {
            try
            {
                var organizationType = await _organizationRepository.GetOrganizationTypeDetail(typeId);
                return _mapper.Map<OrganizationTypeDetailResponseDto>(organizationType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SaveOrganization(SaveOrganizationRequestDto saveOrganizationRequest)
        {
            try
            {
                await _organizationRepository.SaveOrganization(saveOrganizationRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Create_Admin_User",
                    Description = "Admin_User_Created",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    CreatedBy = saveOrganizationRequest.LoggedInUserId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<OrganizationDetailResponseDto> GetOrganizationDetail(string culture, Guid organizationId)
        {
            try
            {
                return await _organizationRepository.GetOrganizationDetail(culture, organizationId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Department
        public async Task SaveDepartment(SaveDepartmentRequestDto saveDepartmentRequest)
        {
            try
            {
                await _organizationRepository.SaveDepartment(saveDepartmentRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<DepartmentsListResponseDto>> GetDepartmentsList(Guid organizationId)
        {
            try
            {
                var departments = await _organizationRepository.GetDepartmentsList(organizationId);
                return _mapper.Map<List<DepartmentsListResponseDto>>(departments);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DepartmentDetailResponseDto> GetDepartmentDetail(Guid departmentId, string culture)
        {
            try
            {
                return await _organizationRepository.GetDepartmentDetail(departmentId, culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Designation
        public async Task SaveDesignation(SaveDesignationRequestDto saveDesignationRequest)
        {
            try
            {
                await _organizationRepository.SaveDesignation(saveDesignationRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DesignationsListResponseDto>> GetDesignationsList()
        {
            try
            {
                var designations = await _organizationRepository.GetDesignationsList();
                return _mapper.Map<List<DesignationsListResponseDto>>(designations);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DesignationDetailResponseDto> GetDesignationDetail(Guid designationId)
        {
            try
            {
                return await _organizationRepository.GetDesignationDetail(designationId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
