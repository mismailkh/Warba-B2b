using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.CarMake.Response;
using WB.Shared.Dtos.ProductPackage.Request;
using WB.Shared.Dtos.ProductPackage.Response;

namespace WB.Application.Interfaces.Services
{
    public interface IProductPackageService
    {
        Task Create(CreateProductPackageRequestDto createProductPackageRequestDto);
        Task<List<GetProductPackageResponseDto>> GetAll();
        Task<GetProductPackageResponseDto> GetById(Guid Id);
        Task<List<GetCarMakeResponseDto>> GetCarMakes(Guid Id);
    }
}
