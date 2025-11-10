using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.LOB;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.Product.RequestDtos;
using WB.Shared.Dtos.Product.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class ProductRepository(DatabaseContext _dbContext, IMapper _mapper) : IProductRepository
    {
        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _dbContext.Product.Where(x => x.IsActive && !x.IsDeleted).ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateProduct(UpdateProductRequestDto request)
        {
            try
            {
                var product = await _dbContext.Product.FindAsync(request.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"No product found against Id : {request.ProductId}");
                }

                product.NameEn = request.NameEn;
                product.NameAr = request.NameAr;
                product.Description = request.Description;
                product.IsActive = request.IsActive;
                product.ModifiedBy = request.ModifiedBy;
                product.ModifiedDate = DateTime.Now;
                _dbContext.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductDetailResponseDto> GetProductDetail(int Id)
        {
            try
            {
                var product = await _dbContext.Product
                    .Include(x => x.ProductProcesses.Where(p => !p.IsDeleted))
                    .ThenInclude(x => x.ProductProcessSubprocesses.Where(p => !p.IsDeleted))
                    .Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new ArgumentException($"No product found against Id : {Id}");
                }

                ProductDetailResponseDto productDetail = _mapper.Map<ProductDetailResponseDto>(product);
                IEnumerable<Process> processes = _dbContext.Process.Where(x => x.IsActive && !x.IsDeleted).ToList();
                IEnumerable<Subprocess> subprocesses = _dbContext.Subprocess.Where(x => x.IsActive && !x.IsDeleted).ToList();
                IEnumerable<ProcessSubprocess> processSubprocesses = _dbContext.ProcessSubprocess.Where(x => !x.IsDeleted).ToList();
                productDetail.ProductProcesses = processes.Select(x => new ProductProcessListResponseDto
                {
                    Id = x.Id,
                    NameEn = x.NameEn,
                    NameAr = x.NameAr,
                    IsAssigned = product.ProductProcesses.Select(p => p.ProcessId).Contains(x.Id),
                    ProductProcessSubprocesses = (from s in subprocesses
                                                  join p in processSubprocesses on s.Id equals p.SubprocessId
                                                  where p.ProcessId == x.Id
                                                  select new ProductProcessSubprocessListResponseDto
                                                 {
                                                     Id = s.Id,
                                                     NameEn = s.NameEn,
                                                     NameAr = s.NameAr,
                                                     IsAssigned = product.ProductProcesses.Where(w => w.ProcessId == x.Id).FirstOrDefault() != null ? product.ProductProcesses.Where(w => w.ProcessId == x.Id).First().ProductProcessSubprocesses.Where(u => u.ProcessSubprocessId == p.Id).Any() : false,
                                                  }).ToList()
                }).ToList();
                return productDetail;

            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
