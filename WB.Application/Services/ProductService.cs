using AutoMapper;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.Product.RequestDtos;
using WB.Shared.Dtos.Product.ResponseDtos;

namespace WB.Application.Services
{
    public class ProductService(IProductRepository _productRepository, IMapper _mapper) : IProductService
    {
        public async Task<List<ProductListResponseDto>> GetAllProducts()
        {
            var result = await _productRepository.GetAllProducts();
            return _mapper.Map<List<ProductListResponseDto>>(result);
        }

        public async Task<ProductDetailResponseDto> GetProductDetail(int Id)
        {
            var result = await _productRepository.GetProductDetail(Id);
            return result;
        }


        public async Task UpdateProduct(UpdateProductRequestDto request)
        {
            await _productRepository.UpdateProduct(request);
        }
    }
}
