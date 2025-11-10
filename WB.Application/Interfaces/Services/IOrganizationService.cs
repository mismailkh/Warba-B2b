using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IOrganizationService
    {
        Task<List<OrganizationTypesListResponseDto>> GetOrganizationTypesList();
        Task UpdateOrganizationType(UpdateOrganizationTypeRequestDto updateOrganizationTypeRequest);
        Task<OrganizationTypeDetailResponseDto> GetOrganizationTypeDetail(int typeId);
        Task SaveOrganization(SaveOrganizationRequestDto saveOrganizationRequest);
        Task<List<OrganizationsListResponseDto>> GetOrganizationsList(int? typeId, string culture);
        Task<OrganizationDetailResponseDto> GetOrganizationDetail(string culture, Guid organizationId);
        Task SaveDepartment(SaveDepartmentRequestDto saveDepartmentRequest);
        Task<DepartmentDetailResponseDto> GetDepartmentDetail(Guid departmentId, string culture);
        Task<List<DepartmentsListResponseDto>> GetDepartmentsList(Guid organizationId);
        Task SaveDesignation(SaveDesignationRequestDto saveDesignationRequest);
        Task<List<DesignationsListResponseDto>> GetDesignationsList();
        Task<DesignationDetailResponseDto> GetDesignationDetail(Guid designationId);
    }
}
