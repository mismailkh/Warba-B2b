using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.CarMake.Response;
using WB.Shared.Dtos.ProductPackage.Request;
using WB.Shared.Dtos.ProductPackage.Response;

namespace WB.Application.Services
{
    public class ProductPackageService(IProductPackageRepository _iProductPackageRepository) : IProductPackageService
    {
        public Task Create(CreateProductPackageRequestDto createProductPackageRequestDto)
        {
            try
            {
                var result = _iProductPackageRepository.Create(createProductPackageRequestDto);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<GetProductPackageResponseDto>> GetAll()
        {
            try
            {
                var result = _iProductPackageRepository.GetAll();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<GetProductPackageResponseDto> GetById(Guid Id)
        {
            try
            {
                var result = _iProductPackageRepository.GetById(Id);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<GetCarMakeResponseDto>> GetCarMakes(Guid Id)
        {
            try
            {
                var result = _iProductPackageRepository.GetCarMakes(Id);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
