using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.MenuItemDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class MenuItemProfile: Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemReadDTO>();
            CreateMap<MenuItemCreateDTO, MenuItem>();
            CreateMap<MenuItemUpdateDTO, MenuItem>();
            CreateMap<MenuItemUpdateDTO, MenuItemReadDTO>().ReverseMap();
        }
    }
}
