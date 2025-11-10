using AutoMapper;
using WB.Domain.Entities.LOB;
using WB.Domain.Entities.Lookups;
using WB.Domain.Entities.Notification;
using WB.Domain.Entities.Organization;
using WB.Domain.Entities.Ums;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;
using WB.Shared.Dtos.Product.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;

namespace WB.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TranslationResponseDto, Translation>().ReverseMap();
            CreateMap<LanguageListResponseDto, Language>().ReverseMap();
            CreateMap<UserPersonalInformation, UserPersonalInformationRequestDto>().ReverseMap();
            CreateMap<User, UserDetailResponseDto>().ReverseMap();
            CreateMap<UserClaims, UserClaimResponseDto>().ReverseMap();

            CreateMap<Group, SaveGroupRequestDto>().ReverseMap();
            CreateMap<Group, GroupDetailResponseDto>().ReverseMap();
            CreateMap<UserGroup, UserGroupResponseDto>().ReverseMap();
            CreateMap<GroupClaims, GroupClaimsResponseDto>().ReverseMap();

            #region Organization
            CreateMap<SaveOrganizationRequestDto, Organization>().ReverseMap();
            CreateMap<Organization, OrganizationDetailResponseDto>()
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items.ContainsKey("culture") && context.Items["culture"].ToString() == "en-US"
                    ? src?.OrganizationType?.NameEn
                    : src?.OrganizationType?.NameAr))
            .ForMember(dest => dest.PaymentMethods, opt => opt.MapFrom(src => src.OrganizationPaymentMethods));
            CreateMap<OrganizationPaymentMethod, OrganizationPaymentMethodsResponseDto>()
            .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => ((GeneralEnums.PaymentMethodEnum)src.MethodId).GetDisplayName()));
            CreateMap<Organization, OrganizationsListResponseDto>()
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items.ContainsKey("culture") && context.Items["culture"].ToString() == "en-US"
                    ? src.OrganizationType.NameEn
                    : src.OrganizationType.NameAr));
            CreateMap<OrganizationType, OrganizationTypesListResponseDto>().ReverseMap();
            CreateMap<OrganizationType, OrganizationTypeDetailResponseDto>().ReverseMap();
            CreateMap<UpdateOrganizationTypeRequestDto, OrganizationType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            #region Department
            CreateMap<SaveDepartmentRequestDto, Department>().ReverseMap();
            CreateMap<Department, DepartmentsListResponseDto>().ReverseMap();
            CreateMap<Department, DepartmentDetailResponseDto>()
            .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizationId))
            .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items.ContainsKey("culture") && context.Items["culture"].ToString() == "en-US"
                    ? src.Organization.NameEn
                    : src.Organization.NameAr))
            .ForMember(dest => dest.OrganizationTypeName, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items.ContainsKey("culture") && context.Items["culture"].ToString() == "en-US"
                    ? src.Organization.OrganizationType.NameEn
                    : src.Organization.OrganizationType.NameAr));
            #endregion

            #region Designation
            CreateMap<SaveDesignationRequestDto, Designation>().ReverseMap();
            CreateMap<Designation, DesignationDetailResponseDto>();
            CreateMap<Designation, DesignationsListResponseDto>().ReverseMap();
            #endregion

            CreateMap<Product, ProductListResponseDto>().ReverseMap();
            CreateMap<Product, ProductDetailResponseDto>().ReverseMap();
            CreateMap<ProductProcess, ProductProcessListResponseDto>().ReverseMap();
            CreateMap<ProductProcessSubprocess, ProductProcessSubprocessListResponseDto>().ReverseMap();

        }
    }
}
