using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationServices.Services.TableManagementService
{
    public interface ITableService
    {
        Task GetAllTablesAsync();
        Task GetTableByIdAsync(int id);
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(int id, Table table);
    }
}
