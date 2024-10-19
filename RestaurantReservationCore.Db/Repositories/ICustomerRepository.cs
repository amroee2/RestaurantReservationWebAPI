using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetCustomersWithBigPartySizeAsync(int partySize);
    }
}