using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.OrderDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>();
            CreateMap<OrderReadDTO, OrderUpdateDTO>();
        }
    }
}
