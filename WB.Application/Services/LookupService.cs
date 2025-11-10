using AutoMapper;
using Microsoft.AspNetCore.Http;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;
using static WB.Shared.Enums.GeneralEnums;

namespace WB.Application.Services
{
    public class LookupService(ILookupRepository _lookupRepository, IMapper _mapper, ILoggingService _iLoggingService, IHttpContextAccessor _httpContext) : ILookupService
    {
        public async Task<Dictionary<string, List<LookupResponseDto>>> GetLookupsData(LookupRequestDto request)
        {
            try
            {
                return await _lookupRepository.GetLookupsData(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<LookupListResponseDto>> GetLookupList(string currentLookupType, int parentId)
        {
            try
            {
                return await _lookupRepository.GetLookupList(currentLookupType, parentId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SaveLookup(SaveLookupRequestDto lookupRequest, string currentLookupType)
        {
            try
            {
                await _lookupRepository.SaveLookup(lookupRequest, currentLookupType);

                #region Logs
                if (lookupRequest.IsDeleted)
                {
                    await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                    {
                        Module = ModuleEnum.Ums.GetDisplayName(),
                        Process = "Delete_Lookup_Process",
                        Description = "User_Deleted_Lookup_Description",
                        IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        LogDate = DateTime.Now,
                        Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                    });
                }
                else
                {
                    await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                    {
                        Module = ModuleEnum.Ums.GetDisplayName(),
                        Process = lookupRequest.Id == 0 ? "Add_New_Lookup_Process" : "Update_Lookup_Process",
                        Description = lookupRequest.Id == 0 ? "User_Added_New_Lookup_Description" : "User_Updated_Lookup_Description",
                        IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        LogDate = DateTime.Now,
                        Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                    });
                }
                #endregion
            }
            catch (Exception ex)
            {
                #region Error Log
                if (lookupRequest.IsDeleted)
                {
                    await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                    {
                        Module = ModuleEnum.Ums.GetDisplayName(),
                        Operation = "Delete_Lookup_Process",
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Source = ex.Source,
                        IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        LogDate = DateTime.Now,
                        Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                    });
                }
                else
                {
                    await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                    {
                        Module = ModuleEnum.Ums.GetDisplayName(),
                        Operation = lookupRequest.Id == 0 ? "Add_New_Lookup_Process" : "Update_Lookup_Process",
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Source = ex.Source,
                        IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        LogDate = DateTime.Now,
                        Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                    });
                }
                #endregion

                throw;
            }
        }
        public async Task<LookupListResponseDto> GetLookupDetail(int Id, string currentLookupType)
        {
            try
            {
                return await _lookupRepository.GetLookupDetail(Id, currentLookupType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
