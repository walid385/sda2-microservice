using AutoMapper;
using SalesService.DTOs;
using SalesService.Models;

namespace SalesService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TicketSystem, TicketSystemDto>().ReverseMap();
            CreateMap<RegistersTable, RegistersTableDto>().ReverseMap();
        }
    }
}
