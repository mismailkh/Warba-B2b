using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using PdfSharp.Drawing.BarCodes;
using System.IdentityModel.Tokens.Jwt;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.AuditLogs;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.General;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class LoggingRepository(IServiceScopeFactory _scopeFactory, DatabaseContext _databaseContext) : ILoggingRepository
    {
        public async Task SaveErrorLogDetails(SaveErrorLogRequestDto errorLogDetails)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            try
            {
                var errorLog = new ErrorLog
                {
                    Id = Guid.NewGuid(),
                    Source = errorLogDetails.Source,
                    Module = errorLogDetails.Module,
                    Computer = Environment.MachineName,
                    CreatedBy = GetUserIdFromToken(errorLogDetails.Token),
                    TerminalId = "C0-B6-F9-1A-D8-89/hostname/MAC address",
                    IPAddress = errorLogDetails.IPAddress,
                    Operation = errorLogDetails.Operation,
                    Message = errorLogDetails.Message,
                    StackTrace = errorLogDetails.StackTrace,
                    LogDate = errorLogDetails.LogDate
                };

                dbContext.ErrorLogs.Add(errorLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateProcessLog(SaveProcessLogRequestDto processLogDetails)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            try
            {
                var processLog = new ProcessLog
                {
                    Id = Guid.NewGuid(),
                    Process = processLogDetails.Process,
                    Module = processLogDetails.Module,
                    Description = processLogDetails.Description,
                    Computer = Environment.MachineName,
                    LogDate = processLogDetails.LogDate,
                    CreatedBy = string.IsNullOrEmpty(processLogDetails.Token) ? processLogDetails.CreatedBy : GetUserIdFromToken(processLogDetails.Token),
                    TerminalId = "C0-B6-F9-1A-D8-89/hostname/MAC address",
                    IPAddress = processLogDetails.IPAddress,
                };
                dbContext.ProcessLogs.Add(processLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Get Username From Token
        private string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return jsonToken?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "Anonymous";
        }
        #endregion

       

        public async Task<List<ListProcessLogResponseDto>> GetProcessLogs(GridPagination gridPagination)
        {
            try
            {
                string a = $"EXEC [app].[GetProcessLogs] @culture";
               return await _databaseContext.ProcessLogResponeDto.FromSqlRaw(a).AsNoTracking().ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ErrorLog>> GetErrorLogs(GridPagination GridPagination)
        {
            try
            {
                using var dbContext = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
                var query = dbContext.ErrorLogs.AsQueryable();
                GridPagination.TotalCount = await query.CountAsync();
                if (GridPagination.PageSize.HasValue && GridPagination.PageSize.Value > 0)
                {
                    query = query.OrderByDescending(x => x.LogDate)
                        .Skip(((GridPagination.PageNumber ?? 1) - 1) * GridPagination.PageSize.Value)
                        .Take(GridPagination.PageSize.Value);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
