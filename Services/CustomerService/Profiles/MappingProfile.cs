using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;

namespace CustomerService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Existing mappings
            CreateMap<CustomerInfo, CustomerInfoDto>();
            CreateMap<CustomerCreateDto, CustomerInfo>();
            CreateMap<CartInProgress, CartInProgressDto>().ReverseMap();
            CreateMap<ItemList, ItemListDto>().ReverseMap();
            CreateMap<GiftCard, GiftCardDto>().ReverseMap();
            CreateMap<ReturnTable, ReturnTableDto>().ReverseMap();

            // New mappings for cart creation
            CreateMap<CreateCartDto, CartInProgress>()
                .ForMember(dest => dest.ItemLists, opt => opt.Ignore()) // Manual mapping in controller
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)); // Set CreatedAt automatically

            CreateMap<CreateItemDto, ItemList>()
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore()) // Default or look up in controller
                .ForMember(dest => dest.CartId, opt => opt.Ignore()); // Set CartId later
        }
    }
}
