using WB.Domain.Entities.LOB;
using WB.Shared.Dtos.Product.RequestDtos;
using WB.Shared.Dtos.Product.ResponseDtos;

namespace WB.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<ProductDetailResponseDto> GetProductDetail(int Id);
        Task UpdateProduct(UpdateProductRequestDto request);
    }
}
