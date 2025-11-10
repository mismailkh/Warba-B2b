using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.ProductPackages;
using WB.Domain.Entities.Vehicles;
using WB.Shared.Dtos.CarMake.Response;
using WB.Shared.Dtos.ProductPackage.Request;
using WB.Shared.Dtos.ProductPackage.Response;

namespace WB.Infrastructure.Repository
{
    public class ProductPackageRepository : IProductPackageRepository
    {
        public Task Create(CreateProductPackageRequestDto createProductPackageRequestDto)
        {
            try
            {
                if (!createProductPackageRequestDto.Id.HasValue || createProductPackageRequestDto.Id == Guid.Empty)
                {
                    ProductPackage productPackage = new ProductPackage
                    {
                        Id = Guid.NewGuid(),
                        NameEn = createProductPackageRequestDto.NameEn,
                        NameAr = createProductPackageRequestDto.NameAr,
                        CarMake = createProductPackageRequestDto.CarMake.Select(cm => new CarMake
                        {
                            Id = cm.Id,
                            CarMakeEn = cm.CarMakeEn,
                            CarMakeAr = cm.CarMakeAr
                        }).ToList(),
                        IsActive = createProductPackageRequestDto.IsActive,
                        AgeOfInsured = createProductPackageRequestDto.AgeOfInsured,
                        PolicyDuration = createProductPackageRequestDto.PolicyDuration,
                        YearMakeFrom = createProductPackageRequestDto.YearMakeFrom,
                        YearMakeTo = createProductPackageRequestDto.YearMakeTo,
                        SiMin = createProductPackageRequestDto.SiMin,
                        SiMax = createProductPackageRequestDto.SiMax,
                        RiskRateMin = createProductPackageRequestDto.RiskRateMin,
                        RiskRateMax = createProductPackageRequestDto.RiskRateMax,
                        Retention = createProductPackageRequestDto.Retention,
                        MinPremium = createProductPackageRequestDto.MinPremium,
                        Commission = createProductPackageRequestDto.Commission,
                        InsuranceFee = createProductPackageRequestDto.InsuranceFee,
                        TandC_En = createProductPackageRequestDto.TandC_En,
                        TandC_Ar = createProductPackageRequestDto.TandC_Ar
                    };

                    _productPackages.Add(productPackage);

                    return Task.CompletedTask;
                }

                var result = _productPackages.FirstOrDefault(pp => pp.Id == createProductPackageRequestDto.Id.Value);

                if (result == null)
                {
                    throw new ArgumentException($"No Product Package found against Id : {createProductPackageRequestDto.Id}");
                }

                result.NameEn = createProductPackageRequestDto.NameEn;
                result.NameAr = createProductPackageRequestDto.NameAr;
                result.CarMake = createProductPackageRequestDto.CarMake.Select(cm => new CarMake
                {
                    Id = cm.Id,
                    CarMakeEn = cm.CarMakeEn,
                    CarMakeAr = cm.CarMakeAr,
                    ModifiedBy = cm.ModifiedBy ?? "Admin",
                    ModifiedDate = DateTime.UtcNow
                }).ToList();
                result.IsActive = createProductPackageRequestDto.IsActive;
                result.AgeOfInsured = createProductPackageRequestDto.AgeOfInsured;
                result.PolicyDuration = createProductPackageRequestDto.PolicyDuration;
                result.YearMakeFrom = createProductPackageRequestDto.YearMakeFrom;
                result.YearMakeTo = createProductPackageRequestDto.YearMakeTo;
                result.SiMin = createProductPackageRequestDto.SiMin;
                result.SiMax = createProductPackageRequestDto.SiMax;
                result.RiskRateMin = createProductPackageRequestDto.RiskRateMin;
                result.RiskRateMax = createProductPackageRequestDto.RiskRateMax;
                result.Retention = createProductPackageRequestDto.Retention;
                result.MinPremium = createProductPackageRequestDto.MinPremium;
                result.Commission = createProductPackageRequestDto.Commission;
                result.InsuranceFee = createProductPackageRequestDto.InsuranceFee;
                result.TandC_En = createProductPackageRequestDto.TandC_En;
                result.TandC_Ar = createProductPackageRequestDto.TandC_Ar;
                result.ModifiedDate = DateTime.UtcNow;
                result.ModifiedBy = createProductPackageRequestDto.ModifiedBy ?? "Admin";

                return Task.CompletedTask;
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
                var result = _productPackages.Where(pp => pp.IsActive).ToList();

                if (result == null || result.Count == 0)
                {
                    throw new ArgumentException("No Product Package found.");
                }

                List<GetProductPackageResponseDto> mappedResult = result.Select(cm => new GetProductPackageResponseDto
                {
                    Id = cm.Id,
                    NameEn = cm.NameEn,
                    NameAr = cm.NameAr,
                    AgeOfInsured = cm.AgeOfInsured,
                    PolicyDuration = cm.PolicyDuration,
                    YearMakeFrom = cm.YearMakeFrom,
                    YearMakeTo = cm.YearMakeTo,
                    SiMin = cm.SiMin,
                    SiMax = cm.SiMax,
                    RiskRateMin = cm.RiskRateMin,
                    RiskRateMax = cm.RiskRateMax,
                    Retention = cm.Retention,
                    MinPremium = cm.MinPremium,
                    Commission = cm.Commission,
                    InsuranceFee = cm.InsuranceFee,
                    TandC_En = cm.TandC_En,
                    TandC_Ar = cm.TandC_Ar
                }).ToList();

                return Task.FromResult(mappedResult);
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
                var result = _productPackages.FirstOrDefault(pp => pp.Id == Id);

                if (result == null)
                {
                    throw new ArgumentException($"No Product Package found against Id : {Id}");
                }

                var mappedResult = new GetProductPackageResponseDto
                {
                    Id = result.Id,
                    NameEn = result.NameEn,
                    NameAr = result.NameAr,
                    AgeOfInsured = result.AgeOfInsured,
                    PolicyDuration = result.PolicyDuration,
                    YearMakeFrom = result.YearMakeFrom,
                    YearMakeTo = result.YearMakeTo,
                    SiMin = result.SiMin,
                    SiMax = result.SiMax,
                    RiskRateMin = result.RiskRateMin,
                    RiskRateMax = result.RiskRateMax,
                    Retention = result.Retention,
                    MinPremium = result.MinPremium,
                    Commission = result.Commission,
                    InsuranceFee = result.InsuranceFee,
                    TandC_En = result.TandC_En,
                    TandC_Ar = result.TandC_Ar
                };

                return Task.FromResult(mappedResult);

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
                var result = _productPackages.FirstOrDefault(pp => pp.Id == Id);

                if (result == null)
                {
                    throw new ArgumentException($"No Product Package found against Id : {Id}");
                }

                var mappedResult = result.CarMake.Select(cm => new GetCarMakeResponseDto
                {
                    Id = cm.Id,
                    CarMakeEn = cm.CarMakeEn,
                    CarMakeAr = cm.CarMakeAr
                }).ToList();

                if (mappedResult.Count == 0 || mappedResult == null)
                {
                    throw new ArgumentException($"No Car Makes found against Product Package Id : {Id}");
                }

                return Task.FromResult(mappedResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<ProductPackage> _productPackages = new List<ProductPackage>
        {
            new ProductPackage
            {
                Id = Guid.NewGuid(),
                NameEn = "Gold",
                NameAr = "تأمين شامل للسيارات",
                CarMake = new List<CarMake>
                {
                    new CarMake { Id = Guid.NewGuid(), CarMakeEn = "Toyota", CarMakeAr = "تويوتا" },
                    new CarMake { Id = Guid.NewGuid(), CarMakeEn = "Honda", CarMakeAr = "هوندا" }
                },
                IsActive = true,
                AgeOfInsured = 25,
                PolicyDuration = 1,
                YearMakeFrom = 2015,
                YearMakeTo = 2024,
                SiMin = 1000,
                SiMax = 5000,
                RiskRateMin = 2,
                RiskRateMax = 5,
                Retention = 10,
                MinPremium = 15000,
                Commission = 5,
                InsuranceFee = 500,
                TandC_En = "Full coverage including theft, accident, and fire.",
                TandC_Ar = "تغطية كاملة تشمل السرقة والحوادث والحريق."
            },
            new ProductPackage
            {
                Id = Guid.NewGuid(),
                NameEn = "Silver",
                NameAr = "حزمة المسؤولية تجاه الطرف الثالث",
                CarMake = new List<CarMake>
                {
                    new CarMake { Id = Guid.NewGuid(), CarMakeEn = "Suzuki", CarMakeAr = "سوزوكي" },
                    new CarMake { Id = Guid.NewGuid(), CarMakeEn = "Hyundai", CarMakeAr = "هيونداي" }
                },
                IsActive = true,
                AgeOfInsured = 21,
                PolicyDuration = 2,
                YearMakeFrom = 2010,
                YearMakeTo = 2023,
                SiMin = 50000,
                SiMax = 300000,
                RiskRateMin = 3,
                RiskRateMax = 6,
                Retention = 15,
                MinPremium = 8000,
                Commission = 7,
                InsuranceFee = 300,
                TandC_En = "Covers third-party bodily injury and property damage only.",
                TandC_Ar = "يغطي إصابة الطرف الثالث والأضرار بالممتلكات فقط."
            }
        };

    }
}
