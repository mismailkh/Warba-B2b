using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Domain.Entities.Vehicles;
using WB.Shared.Dtos.CarMake.Request;
using WB.Shared.Dtos.CarMake.Response;

namespace WB.Application.Services
{
    public class CarMakeService(ICarMakeRepository _iCarMakeRepository) : ICarMakeService
    {

        public Task Create(CreateCarMakeRequestDto createCarMakeRequestDto)
        {
            try
            {
                var result = _iCarMakeRepository.Create(createCarMakeRequestDto);

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<List<GetCarMakeResponseDto>> GetAll()
        {
            try
            {
                var result = _iCarMakeRepository.GetAll();

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public Task<GetCarMakeResponseDto> GetById(Guid Id)
        {
            try
            {
                var result = _iCarMakeRepository.GetById(Id);

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        //public Task Update(UpdateCarMakeRequestDto updateCarMakeRequestDto)
        //{
        //    try
        //    {
        //        var result = _iCarMakeRepository.Update(updateCarMakeRequestDto);

        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public Task AddModel(AddCarModelRequestDto model)
        {
            try
            {
                var result = _iCarMakeRepository.AddModel(model);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<CarModel>> GetAllModels(Guid CarMakeId)
        {
            try
            {
                var result = _iCarMakeRepository.GetAllModels(CarMakeId);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
