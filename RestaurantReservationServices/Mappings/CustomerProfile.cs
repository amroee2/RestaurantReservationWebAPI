using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.CustomerDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile() {
            CreateMap<Customer, CustomerReadDTO>();
            CreateMap<CustomerCreateDTO, Customer>();
        }
    }
}
