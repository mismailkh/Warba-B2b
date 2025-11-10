using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.Discount.Request;
using WB.Shared.Dtos.Discount.Response;

namespace WB.Application.Interfaces.Services
{
    public interface IDiscountService
    {
        Task Create(CreateDiscountRequestDto createDiscountRequestDto);
        Task<List<GetDiscountResponseDto>> GetAll();
        Task<GetDiscountResponseDto> GetById(Guid Id);
        Task AddProduct(Guid DiscountId, AddProductRequestDto addProductRequestDto);
        Task<List<GetProductsResponseDto>> GetProducts(Guid DiscountId);
    }
}
