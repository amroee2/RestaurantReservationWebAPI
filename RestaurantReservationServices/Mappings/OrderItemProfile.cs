using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.OrderItemDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemReadDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>();
            CreateMap<OrderItemUpdateDTO, OrderItem>();
            CreateMap<OrderItemReadDTO, OrderItemUpdateDTO>().ReverseMap();
        }
    }
}
