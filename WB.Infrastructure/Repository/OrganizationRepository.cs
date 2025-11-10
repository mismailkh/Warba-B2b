using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Organization;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class OrganizationRepository(DatabaseContext _dbContext, IMapper _mapper) : IOrganizationRepository
    {
        #region Organization Type
        public async Task<List<OrganizationType>> GetOrganizationTypesList()
        {
            try
            {
                return await _dbContext.OrganizationTypes.AsNoTracking().OrderByDescending(t => t.CreatedDate).ToListAsync();
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
                var organizationType = await _dbContext.OrganizationTypes.FindAsync(updateOrganizationTypeRequest.TypeId);

                if (organizationType != null)
                {
                    _mapper.Map(updateOrganizationTypeRequest, organizationType);
                    organizationType.ModifiedDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<OrganizationType> GetOrganizationTypeDetail(int typeId)
        {
            try
            {
                return await _dbContext.OrganizationTypes.AsNoTracking().FirstOrDefaultAsync(ot => ot.Id == typeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Organization
        public async Task SaveOrganization(SaveOrganizationRequestDto saveOrganizationRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (saveOrganizationRequest.Id == null || saveOrganizationRequest.Id == Guid.Empty)
                        {
                            var newOrganization = _mapper.Map<Organization>(saveOrganizationRequest);
                            newOrganization.Id = Guid.NewGuid();
                            saveOrganizationRequest.Id = newOrganization.Id;
                            newOrganization.CreatedBy = saveOrganizationRequest.LoggedInUserId;
                            newOrganization.CreatedDate = DateTime.Now;
                            newOrganization.IsActive = true;
                            newOrganization.IsDeleted = false;
                            newOrganization.Logo = "N/A";

                            await _dbContext.Organizations.AddAsync(newOrganization);
                        }
                        else
                        {
                            var organization = await _dbContext.Organizations.FindAsync(saveOrganizationRequest.Id);
                            if (organization != null)
                            {
                                _mapper.Map(saveOrganizationRequest, organization);
                                organization.ModifiedBy = saveOrganizationRequest.LoggedInUserId;
                                organization.ModifiedDate = DateTime.Now;
                            }
                        }
                        await SaveOrganizationPaymentMethod(saveOrganizationRequest);
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
        protected async Task SaveOrganizationPaymentMethod(SaveOrganizationRequestDto organizationDetails)
        {
            try
            {
                var existingPaymentMethods = _dbContext.OrganizationPaymentMethods.Where(opm => opm.OrganizationId == organizationDetails.Id.Value);
                _dbContext.OrganizationPaymentMethods.RemoveRange(existingPaymentMethods);

                var paymentMethod = organizationDetails.PaymentMethods.Select(pm => new OrganizationPaymentMethod
                {
                    Id = Guid.NewGuid(),
                    MethodId = pm.MethodId,
                    Code = pm.Code,
                    OrganizationId = organizationDetails.Id.Value,
                    CreatedBy = organizationDetails.LoggedInUserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                }).ToList();

                await _dbContext.OrganizationPaymentMethods.AddRangeAsync(paymentMethod);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<OrganizationsListResponseDto>> GetOrganizationsList(int? typeId, string culture)
        {
            try
            {
                var organizationsQuery = _dbContext.Organizations.Include(o => o.OrganizationType).AsNoTracking();
                if (typeId.HasValue)
                    organizationsQuery = organizationsQuery.Where(o => o.TypeId == typeId.Value);

                var organizationsList = await organizationsQuery.OrderByDescending(o => o.CreatedDate).ToListAsync();

                return _mapper.Map<List<OrganizationsListResponseDto>>(organizationsList, opts =>
                {
                    opts.Items["culture"] = culture;
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
                var organization = await _dbContext.Organizations
                    .Include(o => o.OrganizationType)
                    .Include(opm => opm.OrganizationPaymentMethods)
                    .AsNoTracking().FirstOrDefaultAsync(o => o.Id == organizationId);

                return _mapper.Map<OrganizationDetailResponseDto>(organization, opts =>
                {
                    opts.Items["culture"] = culture;
                });
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
                if (saveDepartmentRequest.Id == null || saveDepartmentRequest.Id == Guid.Empty)
                {
                    var newDepartment = _mapper.Map<Department>(saveDepartmentRequest);
                    newDepartment.CreatedBy = saveDepartmentRequest.LoggedInUser;
                    newDepartment.CreatedDate = DateTime.Now;
                    newDepartment.IsActive = true;
                    newDepartment.IsDeleted = false;

                    await _dbContext.Departments.AddAsync(newDepartment);
                }
                else
                {
                    var department = await _dbContext.Departments.FindAsync(saveDepartmentRequest.Id);
                    if (department != null)
                    {
                        _mapper.Map(saveDepartmentRequest, department);
                        department.ModifiedBy = saveDepartmentRequest.LoggedInUser;
                        department.ModifiedDate = DateTime.Now;
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Department>> GetDepartmentsList(Guid organizationId)
        {
            try
            {
                return await _dbContext.Departments.Where(d => d.OrganizationId == organizationId).OrderByDescending(d => d.CreatedDate).AsNoTracking().ToListAsync();
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
                var department = await _dbContext.Departments
                    .Include(d => d.Organization)
                    .ThenInclude(o => o.OrganizationType)
                    .AsNoTracking().FirstOrDefaultAsync(o => o.Id == departmentId);

                return _mapper.Map<DepartmentDetailResponseDto>(department, opts =>
                {
                    opts.Items["culture"] = culture;
                });
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
                if (saveDesignationRequest.Id == null || saveDesignationRequest.Id == Guid.Empty)
                {
                    var newDesignation = _mapper.Map<Designation>(saveDesignationRequest);
                    newDesignation.CreatedBy = saveDesignationRequest.LoggedInUser;
                    newDesignation.CreatedDate = DateTime.Now;
                    newDesignation.IsActive = true;
                    newDesignation.IsDeleted = false;

                    await _dbContext.Designations.AddAsync(newDesignation);
                }
                else
                {
                    var department = await _dbContext.Designations.FindAsync(saveDesignationRequest.Id);
                    if (department != null)
                    {
                        _mapper.Map(saveDesignationRequest, department);
                        department.ModifiedBy = saveDesignationRequest.LoggedInUser;
                        department.ModifiedDate = DateTime.Now;
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Designation>> GetDesignationsList()
        {
            try
            {
                return await _dbContext.Designations.AsNoTracking().OrderByDescending(d => d.CreatedDate).ToListAsync();
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
                var designationDetail = await _dbContext.Designations.AsNoTracking().FirstOrDefaultAsync(o => o.Id == designationId);

                return _mapper.Map<DesignationDetailResponseDto>(designationDetail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
