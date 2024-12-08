using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.EmployeeDTOs;

namespace RestaurantReservationServices.Mappings
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDTO>();
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>();
            CreateMap<EmployeeReadDTO, EmployeeUpdateDTO>().ReverseMap();
        }
    }
}
