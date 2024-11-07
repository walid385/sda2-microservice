using AutoMapper;
using OrderManagementService.DTOs;
using Events;
using OrderManagementService.Models;

namespace OrderManagementService
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Mapping from DTO to Order model
            CreateMap<CreateOrderDto, Order>();

            // Mapping from Order model to OrderCreatedEvent for publishing events
            CreateMap<Order, OrderCreatedEvent>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            // Mapping from Order model to OrderDto for returning to clients
            CreateMap<Order, OrderDto>();

            CreateMap<UpdateOrderDto, Order>();
            CreateMap<Order, OrderUpdatedEvent>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
