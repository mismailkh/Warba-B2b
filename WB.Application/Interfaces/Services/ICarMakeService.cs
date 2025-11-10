using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Domain.Entities.Vehicles;
using WB.Shared.Dtos.CarMake.Request;
using WB.Shared.Dtos.CarMake.Response;

namespace WB.Application.Interfaces.Services
{
    public interface ICarMakeService
    {
        Task Create(CreateCarMakeRequestDto createCarMakeRequestDto);
        Task<List<GetCarMakeResponseDto>> GetAll();
        Task<GetCarMakeResponseDto> GetById(Guid Id);
        //Task Update(UpdateCarMakeRequestDto updateCarMakeRequestDto);
        Task AddModel(AddCarModelRequestDto model);
        Task<List<CarModel>> GetAllModels(Guid CarMakeId);
    }
}
