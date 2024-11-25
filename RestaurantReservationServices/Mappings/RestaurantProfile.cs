using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.RestaurantDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class RestaurantProfile: Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<RestaurantCreateDTO, Restaurant>();
            CreateMap<RestaurantUpdateDTO, Restaurant>();
        }
    }
}
