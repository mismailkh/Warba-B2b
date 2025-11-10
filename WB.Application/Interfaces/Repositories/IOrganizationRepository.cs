using WB.Domain.Entities.Organization;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Application.Interfaces.Repositories
{
    public interface IOrganizationRepository
    {
        Task<List<OrganizationType>> GetOrganizationTypesList();
        Task UpdateOrganizationType(UpdateOrganizationTypeRequestDto updateOrganizationTypeRequest);
        Task<List<OrganizationsListResponseDto>> GetOrganizationsList(int? typeId, string culture);
        Task<OrganizationType> GetOrganizationTypeDetail(int typeId);
        Task SaveOrganization(SaveOrganizationRequestDto saveOrganizationRequest);
        Task<OrganizationDetailResponseDto> GetOrganizationDetail(string culture, Guid organizationId);
        Task SaveDepartment(SaveDepartmentRequestDto saveDepartmentRequest);
        Task<List<Department>> GetDepartmentsList(Guid organizationId);
        Task<DepartmentDetailResponseDto> GetDepartmentDetail(Guid departmentId, string culture);
        Task SaveDesignation(SaveDesignationRequestDto saveDesignationRequest);
        Task<List<Designation>> GetDesignationsList();
        Task<DesignationDetailResponseDto> GetDesignationDetail(Guid designationId);
    }
}
