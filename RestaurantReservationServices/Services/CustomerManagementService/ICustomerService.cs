using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.CustomerDTOs;

namespace RestaurantReservationServices.Services.CustomerManagementService
{
    public interface ICustomerService
    {
        Task<List<CustomerReadDTO>> GetAllCustomersAsync();
        Task<CustomerReadDTO> GetCustomerByIdAsync(int id);
        Task<int> AddCustomerAsync(CustomerCreateDTO customer);
        Task UpdateCustomerAsync(int Id, Customer customer);
        Task DeleteCustomerAsync(int Id);
        Task GetCustomersWithBigPartySizeAsync(int partySize);
    }
}
