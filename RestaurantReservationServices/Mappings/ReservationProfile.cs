using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.ReservationDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class ReservationProfile: Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationReadDTO>();
            CreateMap<ReservationCreateDTO, Reservation>();
            CreateMap<ReservationUpdateDTO, Reservation>();
            CreateMap<ReservationReadDTO, ReservationUpdateDTO>();
        }
    }
}
