using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Domain.Entities.LOB;
using WB.Shared.Dtos.Product.RequestDtos;
using WB.Shared.Dtos.Product.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductListResponseDto>> GetAllProducts();
        Task<ProductDetailResponseDto> GetProductDetail(int Id);
        Task UpdateProduct(UpdateProductRequestDto request);
    }
}
