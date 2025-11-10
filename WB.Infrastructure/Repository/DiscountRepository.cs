using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Discounts;
using WB.Domain.Entities.LOB;
using WB.Shared.Dtos.Discount.Request;
using WB.Shared.Dtos.Discount.Response;

namespace WB.Infrastructure.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        public Task Create(CreateDiscountRequestDto createDiscountRequestDto)
        {
            try
            {
                if (!createDiscountRequestDto.Id.HasValue || createDiscountRequestDto.Id == Guid.Empty)
                {
                    Discount discount = new Discount
                    {
                        Id = Guid.NewGuid(),
                        NameEn = createDiscountRequestDto.NameEn,
                        NameAr = createDiscountRequestDto.NameAr,
                        Percentage = createDiscountRequestDto.Disc_Percentage,
                        FromDate = createDiscountRequestDto.FromDate,
                        ToDate = createDiscountRequestDto.ToDate,
                        Description = createDiscountRequestDto.Description,
                        IsActive = createDiscountRequestDto.IsActive
                    };

                    _discounts.Add(discount);

                    return Task.CompletedTask;
                }

                var result = _discounts.FirstOrDefault(d => d.Id == createDiscountRequestDto.Id.Value);

                if (result == null)
                {
                    throw new ArgumentException($"No Product Package found against Id : {createDiscountRequestDto.Id}");
                }

                result.NameEn = createDiscountRequestDto.NameEn;
                result.NameAr = createDiscountRequestDto.NameAr;
                result.Percentage = createDiscountRequestDto.Disc_Percentage;
                result.FromDate = createDiscountRequestDto.FromDate;
                result.ToDate = createDiscountRequestDto.ToDate;
                result.Description = createDiscountRequestDto.Description;
                result.IsActive = createDiscountRequestDto.IsActive;
                result.ModifiedBy = "Admin";
                result.ModifiedDate = DateTime.UtcNow;

                return Task.CompletedTask;
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
                var result = _discounts.Where(d => d.IsActive).ToList();

                if (result == null || result.Count == 0)
                {
                    throw new ArgumentException("No Discount found.");
                }

                List<GetDiscountResponseDto> mappedResult = result.Select(d => new GetDiscountResponseDto
                {
                    Id = d.Id,
                    NameEn = d.NameEn,
                    NameAr = d.NameAr,
                    Disc_Percentage = d.Percentage,
                    FromDate = d.FromDate,
                    ToDate = d.ToDate,
                    Description = d.Description,
                    IsActive = d.IsActive
                }).ToList();


                return Task.FromResult(mappedResult);
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
                var result = _discounts.FirstOrDefault(d => d.Id == Id);

                if (result == null)
                {
                    throw new ArgumentException($"No Product Package found against Id : {Id}");
                }

                var mappedResult = new GetDiscountResponseDto
                {
                    Id = result.Id,
                    NameEn = result.NameEn,
                    NameAr = result.NameAr,
                    Disc_Percentage = result.Percentage,
                    FromDate = result.FromDate,
                    ToDate = result.ToDate,
                    Description = result.Description,
                    IsActive = result.IsActive
                };

                return Task.FromResult(mappedResult);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task AddProduct(Guid DiscountId, AddProductRequestDto addProductRequestDto)
        {
            try
            {
                var discount = _discounts.FirstOrDefault(d => d.Id == DiscountId);

                if (discount == null)
                {
                    throw new ArgumentException($"No Discount found against Id : {DiscountId}");
                }

                Product product = new Product
                {
                    Id = addProductRequestDto.Id,
                    NameEn = addProductRequestDto.NameEn,
                    NameAr = addProductRequestDto.NameAr,
                    IsActive = addProductRequestDto.IsActive,
                    //Processes = addProductRequestDto.Processes
                };

                discount.Products.Add(product);

                return Task.CompletedTask;
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
                var discount = _discounts.FirstOrDefault(d => d.Id == DiscountId);

                if (discount == null)
                {
                    throw new ArgumentException($"No Discount found against Id : {DiscountId}");
                }

                var products = discount.Products;

                List<GetProductsResponseDto> mappedResult = products.Select(p => new GetProductsResponseDto
                {
                    Id = p.Id,
                    NameEn = p.NameEn,
                    NameAr = p.NameAr,
                    IsActive = p.IsActive,
                    //Processes = p.Processes.Where(pr => (pr.IsDiscount && pr.IsActive)).ToList()
                }).ToList();

                return Task.FromResult(mappedResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<Discount> _discounts = new List<Discount>
        {
            new Discount
            {
                Id = Guid.NewGuid(),
                NameEn = "Winter Special Offer",
                NameAr = "عرض الشتاء الخاص",
                Percentage = 15,
                FromDate = new DateOnly(2025, 12, 1),
                ToDate = new DateOnly(2026, 2, 28),
                Description = "Get 15% off on all vehicle insurance renewals this winter season.",
                IsActive = true,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        NameEn = "Comprehensive Car Insurance",
                        NameAr = "تأمين السيارة الشامل",
                        IsActive = true,
                        //Processes = new List<ProductProcess>
                        //{
                        //    new ProductProcess { ProcessTypeId = (int)ProcessType.Renew, IsActive = true, IsDiscount = true },
                        //    new ProductProcess { ProcessTypeId = (int)ProcessType.Purchase, IsActive = true, IsDiscount = false }
                        //}
                    }
                }
            },
            new Discount
            {
                Id = Guid.NewGuid(),
                NameEn = "New Year Deal",
                NameAr = "صفقة السنة الجديدة",
                Percentage = 20,
                FromDate = new DateOnly(2026, 1, 1),
                ToDate = new DateOnly(2026, 1, 15),
                Description = "Celebrate the new year with a 20% discount on new policy purchases.",
                IsActive = true,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 2,
                        NameEn = "Third Party Insurance",
                        NameAr = "تأمين الطرف الثالث",
                        IsActive = true,
                        //Processes = new List<ProductProcess>
                        //{
                        //    new ProductProcess { ProcessTypeId = (int)ProcessType.Purchase, IsActive = true, IsDiscount = true },
                        //    new ProductProcess { ProcessTypeId = (int)ProcessType.QouteGeneration, IsActive = true, IsDiscount = false }
                        //}
                    }
                }
            },
            new Discount
            {
                Id = Guid.NewGuid(),
                NameEn = "Summer End Clearance",
                NameAr = "تخفيضات نهاية الصيف",
                Percentage = 10,
                FromDate = new DateOnly(2025, 8, 1),
                ToDate = new DateOnly(2025, 9, 15),
                Description = "Exclusive 10% discount on ownership transfer processing fees.",
                IsActive = false,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 3,
                        NameEn = "Ownership Transfer Service",
                        NameAr = "خدمة نقل الملكية",
                        IsActive = true,
                        //Processes = new List<ProductProcess>
                        //{
                        //    new ProductProcess { ProcessTypeId = (int)ProcessType.TransferOfOwnership, IsActive = true, IsDiscount = true }
                        //}
                    }
                }
            }
        };

    }
}
