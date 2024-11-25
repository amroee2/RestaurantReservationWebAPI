using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationServices.Services.TableManagementService
{
    public class TableService: ITableService
    {
        private readonly IRepository<Table> _tableRepository;

        public TableService(IRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            if (!tables.Any())
            {
                Console.WriteLine("There are currently no tables");
                return;
            }
            foreach (var table in tables)
            {
                Console.WriteLine(table);
            }
        }

        public async Task GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                Console.WriteLine("Table doesn't exist");
                return;
            }
            Console.WriteLine(table);
        }

        public async Task AddTableAsync(Table table)
        {
            var existingTable = await _tableRepository.GetByIdAsync(table.TableId);
            if (existingTable != null)
            {
                Console.WriteLine("Table already exists");
                return;
            }
            await _tableRepository.AddAsync(table);
        }

        public async Task UpdateTableAsync(int id, Table table)
        {
            var updatedTable = await _tableRepository.GetByIdAsync(id);
            if (updatedTable == null)
            {
                Console.WriteLine("Table doesn't exist");
                return;
            }
            updatedTable.Capacity = table.Capacity;
            updatedTable.RestaurantId = table.RestaurantId;
            await _tableRepository.UpdateAsync(updatedTable);
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                Console.WriteLine("Table doesn't exist");
                return;
            }
            await _tableRepository.DeleteAsync(table);
        }
    }
}