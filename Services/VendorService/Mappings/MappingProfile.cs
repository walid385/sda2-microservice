using AutoMapper;
using VendorService.Models;
using VendorService.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vendor, VendorDto>().ReverseMap();
    }
}
