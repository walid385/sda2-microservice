using AutoMapper;
using InventoryService.DTOs;
using InventoryService.Models;

namespace InventoryService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductInventory, ProductInventoryDto>().ReverseMap();
        }
    }
}
