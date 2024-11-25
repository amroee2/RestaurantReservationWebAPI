using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.CustomerDTOs;

namespace RestaurantReservationServices.Services.CustomerManagementService
{
    public interface ICustomerService
    {
        Task<List<CustomerReadDTO>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(int Id, Customer customer);
        Task DeleteCustomerAsync(int Id);
        Task GetCustomersWithBigPartySizeAsync(int partySize);
    }
}
