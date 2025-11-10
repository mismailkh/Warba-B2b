using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Vehicles;
using WB.Shared.Dtos.CarMake.Request;
using WB.Shared.Dtos.CarMake.Response;

namespace WB.Infrastructure.Repository
{
    public class CarMakeRepository : ICarMakeRepository
    {
        //private readonly IMapper _mapper;

        //public CarMakeRepository(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}

        public Task<List<GetCarMakeResponseDto>> GetAll()
        {
            try
            {
                var result = _carMakes.Where(cm => cm.IsActive).ToList();

                if(result == null || result.Count == 0)
                {
                    throw new ArgumentException("No Car Makes found.");
                }

                //var mappedResult = _mapper.Map<List<GetCarMakeResponseDto>>(result);

                List<GetCarMakeResponseDto> mappedResult = result.Select(cm => new GetCarMakeResponseDto
                {
                    Id = cm.Id,
                    CarMakeEn = cm.CarMakeEn,
                    CarMakeAr = cm.CarMakeAr,
                    Description = cm.Description,
                    IsActive = cm.IsActive
                }).ToList();

                return Task.FromResult(mappedResult);
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
                var result = _carMakes.FirstOrDefault(cm => cm.Id == Id);

                if (result == null)
                {
                    throw new ArgumentException($"No Car Make found against Id : {Id}");
                }

                var mappedResult = new GetCarMakeResponseDto
                {
                    Id = result.Id,
                    CarMakeEn = result.CarMakeEn,
                    CarMakeAr = result.CarMakeAr,
                    Description = result.Description,
                    IsActive = result.IsActive
                };

                return Task.FromResult(mappedResult);

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task Create(CreateCarMakeRequestDto createCarMakeRequestDto)
        {
            try
            {
                if(!createCarMakeRequestDto.Id.HasValue || createCarMakeRequestDto.Id.Value == Guid.Empty)
                {
                    CarMake newCarMake = new CarMake
                    {
                        Id = Guid.NewGuid(),
                        CarMakeEn = createCarMakeRequestDto.CarMakeEn,
                        CarMakeAr = createCarMakeRequestDto.CarMakeAr,
                        Description = createCarMakeRequestDto.Description,
                        IsActive = createCarMakeRequestDto.IsActive ?? false
                    };

                    _carMakes.Add(newCarMake);

                    return Task.CompletedTask;
                }

                var carMake = _carMakes.FirstOrDefault(cm => cm.Id == createCarMakeRequestDto.Id);

                if (carMake == null)
                {
                    throw new ArgumentException($"No Car Make found against Id : {createCarMakeRequestDto.Id}");
                }

                carMake.CarMakeEn = createCarMakeRequestDto.CarMakeEn;
                carMake.CarMakeAr = createCarMakeRequestDto.CarMakeAr;
                carMake.Description = createCarMakeRequestDto.Description ?? carMake.Description;
                carMake.IsActive = createCarMakeRequestDto.IsActive ?? carMake.IsActive;
                carMake.ModifiedDate = DateTime.UtcNow;
                carMake.ModifiedBy = "Admin";

                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task AddModel(AddCarModelRequestDto model)
        {
            try
            {
                var carMake = _carMakes.FirstOrDefault(cm => cm.Id == model.CarMakeId);

                if (carMake == null)
                {
                    throw new ArgumentException($"No Car Make found against Id : {model.CarMakeId}");
                }

                CarModel carModel = new CarModel
                {
                    Id =  Guid.NewGuid(),
                    CarModelEn = model.CarModelEn,
                    CarModelAr = model.CarModelAr,
                    Description = model.Description,
                    IsActive = model.IsActive
                };

                carMake.CarModels ??= new List<CarModel>();

                carMake.CarModels.Add(carModel);

                return Task.CompletedTask;

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<List<CarModel>> GetAllModels(Guid CarMakeId)
        {
            try
            {
                var carMake = _carMakes.FirstOrDefault(cm => cm.Id == CarMakeId);
                if (carMake == null)
                {
                    throw new ArgumentException($"No Car Make found against Id : {CarMakeId}");
                }

                var models = carMake.CarModels.ToList();

                if(models == null || models.Count == 0)
                {
                    throw new ArgumentException($"No Car Models found for Car Make Id : {CarMakeId}");
                }

                return Task.FromResult(models);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private static List<CarMake> _carMakes = new List<CarMake>
        {
            new CarMake
            {
                Id = Guid.NewGuid(),
                CarMakeEn = "Toyota",
                CarMakeAr = "تويوتا",
                Description = "Japanese automobile manufacturer known for reliability.",
                IsActive = true,
                CarModels = new List<CarModel>
                {
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Corolla",
                        CarModelAr = "كورولا",
                        Description = "Compact sedan",
                        IsActive = true
                    },
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Camry",
                        CarModelAr = "كامري",
                        Description = "Mid-size sedan",
                        IsActive = true
                    },
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Yaris",
                        CarModelAr = "يارس",
                        Description = "Small economical car",
                        IsActive = true
                    }
                }
            },
            new CarMake
            {
                Id = Guid.NewGuid(),
                CarMakeEn = "Honda",
                CarMakeAr = "هوندا",
                Description = "Japanese brand known for performance and fuel efficiency.",
                IsActive = true,
                CarModels = new List<CarModel>
                {
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Civic",
                        CarModelAr = "سيفيك",
                        Description = "Compact car popular worldwide",
                        IsActive = true
                    },
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Accord",
                        CarModelAr = "اكورد",
                        Description = "Mid-size luxury sedan",
                        IsActive = true
                    }
                }
            },
            new CarMake
            {
                Id = Guid.NewGuid(),
                CarMakeEn = "Hyundai",
                CarMakeAr = "هيونداي",
                Description = "South Korean manufacturer offering affordable vehicles.",
                IsActive = true,
                CarModels = new List<CarModel>
                {
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Elantra",
                        CarModelAr = "الانترا",
                        Description = "Compact sedan",
                        IsActive = true
                    },
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Tucson",
                        CarModelAr = "توسان",
                        Description = "Compact SUV",
                        IsActive = true
                    }
                }
            },
            new CarMake
            {
                Id = Guid.NewGuid(),
                CarMakeEn = "Suzuki",
                CarMakeAr = "سوزوكي",
                Description = "Japanese brand popular for small affordable cars.",
                IsActive = true,
                CarModels = new List<CarModel>
                {
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Alto",
                        CarModelAr = "التو",
                        Description = "Mini car for city use",
                        IsActive = true
                    },
                    new CarModel
                    {
                        Id = Guid.NewGuid(),
                        CarModelEn = "Cultus",
                        CarModelAr = "كلتس",
                        Description = "Hatchback family car",
                        IsActive = true
                    }
                }
            }
        };

    }
}
