using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationServices.DTOs.TableDTOs;

namespace RestaurantReservationServices.Services.TableManagementService
{
    public interface ITableService
    {
        Task<List<TableReadDTO>> GetAllTablesAsync();
        Task<TableReadDTO> GetTableByIdAsync(int id);
        Task<int> AddTableAsync(TableCreateDTO table);
        Task UpdateTableAsync(int id, TableUpdateDTO table);
        Task DeleteTableAsync(int id);
    }
}
