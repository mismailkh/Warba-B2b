using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Application.Interfaces.Services
{
    public interface ILookupService
    {
        Task<Dictionary<string, List<LookupResponseDto>>> GetLookupsData(LookupRequestDto request);
        Task<List<LookupListResponseDto>> GetLookupList(string currentLookupType, int parentId);
        Task SaveLookup(SaveLookupRequestDto lookupRequest, string currentLookupType);
        Task<LookupListResponseDto> GetLookupDetail(int Id, string currentLookupType);
    }
}
