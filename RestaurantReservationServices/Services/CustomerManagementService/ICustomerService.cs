using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.CustomerManagementService
{
    public interface ICustomerService
    {
        Task GetAllCustomersAsync();
        Task GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(int Id, Customer customer);
        Task DeleteCustomerAsync(int Id);
        Task GetCustomersWithBigPartySizeAsync(int partySize);
    }
}
