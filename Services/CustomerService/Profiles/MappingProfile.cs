using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;

namespace CustomerService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerInfo, CustomerInfoDto>();
            CreateMap<CustomerCreateDto, CustomerInfo>();
            CreateMap<CartInProgress, CartInProgressDto>().ReverseMap();
            CreateMap<ItemList, ItemListDto>().ReverseMap();
            CreateMap<GiftCard, GiftCardDto>().ReverseMap();
            CreateMap<ReturnTable, ReturnTableDto>().ReverseMap();
        }
    }
}
