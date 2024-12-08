using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.TableDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class TableProfile: Profile
    {
        public TableProfile()
        {
            CreateMap<Table, TableReadDTO>();
            CreateMap<TableCreateDTO, Table>();
            CreateMap<TableUpdateDTO, Table>();
            CreateMap<TableReadDTO, TableUpdateDTO>().ReverseMap();
        }
    }
}
