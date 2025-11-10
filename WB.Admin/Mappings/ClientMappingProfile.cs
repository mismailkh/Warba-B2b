using AutoMapper;
using WB.Shared.Dtos.Organization.RequestDtos;
using WB.Shared.Dtos.Organization.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Admin.Mappings
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<UserDetailResponseDto, SaveUserRequestDto>().ReverseMap();
            CreateMap<UserPersonalInformationResponseDto, UserPersonalInformationRequestDto>().ReverseMap();
            CreateMap<UserContactResponseDto, UserContactRequestDto>().ReverseMap();
            CreateMap<UserAddressResponseDto, UserAddressRequestDto>().ReverseMap();
            CreateMap<GroupListResponseDto, SaveGroupRequestDto>().ReverseMap();
            CreateMap<GroupDetailResponseDto, SaveGroupRequestDto>().ReverseMap();
            CreateMap<SaveGroupRequestDto, GroupListResponseDto>().ReverseMap();
            CreateMap<RoleDetailResponseDto, SaveRoleRequestDto>().ReverseMap();
            CreateMap<SaveRoleRequestDto, RolesListResponseDto>().ReverseMap();
            CreateMap<NotificationEventResponseDto, UpdateNotificationEventRequestDto>().ReverseMap();
            CreateMap<NotificationTemplateResponseDto, UpdateNotificationTemplateRequestDto>();
            CreateMap<UpdateNotificationTemplateRequestDto, NotificationTemplateResponseDto>()
                .ForMember(dest => dest.EventId, opt => opt.Ignore())
                .ForMember(dest => dest.EventName, opt => opt.Ignore())
                .ForMember(dest => dest.ChannelName, opt => opt.Ignore())
                .ForMember(dest => dest.TemplateId, opt => opt.Ignore());
            CreateMap<LookupListResponseDto, SaveLookupRequestDto>().ReverseMap();

            #region Organization
            CreateMap<OrganizationDetailResponseDto, SaveOrganizationRequestDto>()
                .ForMember(dest => dest.PaymentMethods, opt => opt.MapFrom(src => src.PaymentMethods)).ReverseMap();
            CreateMap<OrganizationPaymentMethodsResponseDto, SaveOrganizationPaymentMethodRequestDto>().ReverseMap();
            CreateMap<OrganizationTypeDetailResponseDto, UpdateOrganizationTypeRequestDto>().ReverseMap();
            #endregion

            #region Department
            CreateMap<DepartmentDetailResponseDto, SaveDepartmentRequestDto>().ReverseMap();
            #endregion
        }
    }
}
