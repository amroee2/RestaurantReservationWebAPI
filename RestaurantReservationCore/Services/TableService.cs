using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservation.Services
{
    public class TableService
    {
        private readonly IRepository<Table> _tableRepository;

        public TableService(IRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<List<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllAsync();
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _tableRepository.GetByIdAsync(id);
        }

        public async Task AddTableAsync(Table table)
        {
            await _tableRepository.AddAsync(table);
        }

        public async Task UpdateTableAsync(Table table)
        {
            await _tableRepository.UpdateAsync(table);
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                Console.WriteLine("Table doesn't exist");
            }
            await _tableRepository.DeleteAsync(table);
        }
    }
}
