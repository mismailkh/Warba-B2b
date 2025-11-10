using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using WB.Application.Interfaces.Repositories;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class LookupRepository(DatabaseContext _dbContext, IMapper _mapper) : ILookupRepository
    {
        public async Task<Dictionary<string, List<LookupResponseDto>>> GetLookupsData(LookupRequestDto request)
        {
            try
            {
                var result = new Dictionary<string, List<LookupResponseDto>>(StringComparer.OrdinalIgnoreCase);

                foreach (var tableRequest in request.Tables)
                {
                    var dbSetProperty = _dbContext.GetType().GetProperty(tableRequest.Table);
                    if (dbSetProperty == null)
                    {
                        throw new ArgumentException($"Invalid table name: {tableRequest.Table}");
                    }

                    var dbSet = dbSetProperty.GetValue(_dbContext) as IQueryable<object>;
                    if (dbSet == null)
                    {
                        throw new ArgumentException($"Unable to retrieve data for: {tableRequest.Table}");
                    }

                    var query = dbSet.Where(entity =>
                        EF.Property<bool>(entity, "IsActive") && !EF.Property<bool>(entity, "IsDeleted"));

                    if (!string.IsNullOrEmpty(tableRequest.FilterColumn) && tableRequest.FilterValue != null)
                    {
                        var parameter = Expression.Parameter(typeof(object), "e");
                        var property = Expression.Call(
                            typeof(EF), "Property", new[] { typeof(object) },
                            parameter, Expression.Constant(tableRequest.FilterColumn)
                        );
                        object filterValue = ConvertFilterValue(tableRequest.FilterValue);

                        if (filterValue != null)
                        {
                            var predicate = Expression.Lambda<Func<object, bool>>(
                                Expression.Equal(Expression.Convert(property, filterValue.GetType()),
                                Expression.Constant(filterValue, filterValue.GetType())),
                                parameter
                            );
                            query = query.Where(predicate);
                        }
                    }

                    var lookupData = await query.Select(entity => new LookupResponseDto
                    {
                        Id = EF.Property<int>(entity, "Id"),
                        Name = request.Culture == "en-US" ? EF.Property<string>(entity, "NameEn") : EF.Property<string>(entity, "NameAr"),
                        IsActive = EF.Property<Boolean>(entity, "IsActive"),
                        ExtraData = tableRequest.ExtraColumns.Any() ? tableRequest.ExtraColumns
                   .ToDictionary(col => col, col => (object)EF.Property<object>(entity, col)) : new Dictionary<string, object>()
                    }).OrderBy(x => x.Name).ToListAsync();

                    result[tableRequest.Table] = lookupData;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private object ConvertFilterValue(object value)
        {
            if (value == null) return null;

            if (value is JsonElement jsonElement)
            {
                return jsonElement.ValueKind switch
                {
                    JsonValueKind.String => jsonElement.GetString(),
                    JsonValueKind.Number => jsonElement.TryGetInt32(out var intValue) ? intValue
                        : jsonElement.TryGetInt64(out var longValue) ? longValue
                        : jsonElement.TryGetDouble(out var doubleValue) ? doubleValue
                        : jsonElement.GetDecimal(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    _ => throw new InvalidOperationException("Unsupported JSON type for filtering")
                };
            }

            return value; // If already correct type (int, bool, etc.), return as is.
        }

        public async Task<List<LookupListResponseDto>> GetLookupList(string currentLookupType, int parentId)
        {
            try
            {
                currentLookupType = "USER_" + currentLookupType.ToUpper() + "_LKP";
                return await _dbContext.LookupListResponseDto.FromSql($"select * from ums.\"fLookupList\"({currentLookupType},{parentId})").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveLookup(SaveLookupRequestDto lookupRequest, string currentLookupType)
        {
            using (_dbContext)
            {
                try
                {
                    Assembly currentAssembly = Assembly.GetExecutingAssembly();
                    var LoadedAssembly = Assembly.Load(currentAssembly.GetReferencedAssemblies().Where(x => x.FullName.Contains("WB.Domain")).FirstOrDefault());
                    string entityName = "WB.Domain.Entities.Lookups." + currentLookupType;
                    Type type = LoadedAssembly.GetType(entityName);
                    if (lookupRequest.Id == 0)
                    {
                        var lookupmapping = _mapper.Map(lookupRequest, typeof(SaveLookupRequestDto), type);
                        await _dbContext.AddAsync(lookupmapping);
                    }
                    else
                    {
                        var dbSet = _dbContext.GetType().GetProperty(type.Name).GetValue(_dbContext);
                        var entity = await ((dynamic)dbSet).FindAsync(lookupRequest.Id);
                        _mapper.Map(lookupRequest, entity);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<LookupListResponseDto> GetLookupDetail(int Id, string currentLookupType)
        {
            try
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                var LoadedAssembly = Assembly.Load(currentAssembly.GetReferencedAssemblies().Where(x => x.FullName.Contains("WB.Domain")).FirstOrDefault());
                string entityName = "WB.Domain.Entities.Lookups." + currentLookupType;
                Type type = LoadedAssembly.GetType(entityName);

                var dbSet = _dbContext.GetType().GetProperty(type.Name).GetValue(_dbContext);
                var entity = await ((dynamic)dbSet).FindAsync(Id);
                return _mapper.Map<LookupListResponseDto>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
