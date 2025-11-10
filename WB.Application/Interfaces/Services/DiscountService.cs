using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Application.Interfaces.Repositories;
using WB.Shared.Dtos.Discount.Request;
using WB.Shared.Dtos.Discount.Response;

namespace WB.Application.Interfaces.Services
{
    public class DiscountService(IDiscountRepository _iDiscountRepository) : IDiscountService
    {
        public Task AddProduct(Guid DiscountId, AddProductRequestDto addProductRequestDto)
        {
            try
            {
                var result = _iDiscountRepository.AddProduct(DiscountId, addProductRequestDto);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task Create(CreateDiscountRequestDto createDiscountRequestDto)
        {
            try
            {
                var result = _iDiscountRepository.Create(createDiscountRequestDto);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<GetDiscountResponseDto>> GetAll()
        {
            try
            {
                var result = _iDiscountRepository.GetAll();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<GetDiscountResponseDto> GetById(Guid Id)
        {
            try
            {
                var result = _iDiscountRepository.GetById(Id);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<GetProductsResponseDto>> GetProducts(Guid DiscountId)
        {
            try
            {
                var result = _iDiscountRepository.GetProducts(DiscountId);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
