using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class TableUI
    {
        private readonly TableService _tableService;

        public TableUI(TableService tableService)
        {
            _tableService = tableService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                Console.WriteLine("1. Add Table");
                Console.WriteLine("2. Update Table");
                Console.WriteLine("3. Delete Table");
                Console.WriteLine("4. View All Tables");
                Console.WriteLine("5. View Table By Id");
                Console.WriteLine("0. Go Back");
                try
                {
                    Enum.TryParse(Console.ReadLine(), out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    await HandleRequestAsync(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task HandleRequestAsync(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    await AddTableAsync();
                    break;

                case OperationOptions.Update:
                    await UpdateTableAsync();
                    break;

                case OperationOptions.Delete:
                    await DeleteTableAsync();
                    break;

                case OperationOptions.View:
                    await ViewAllTablesAsync();
                    break;

                case OperationOptions.Search:
                    await ViewTableByIdAsync();
                    break;
            }
        }

        public async Task AddTableAsync()
        {
            Console.WriteLine("Enter table capacity");
            int cpacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter restaurant id");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            var table = new Table
            {
                Capacity = cpacity,
                RestaurantId = restaurantId
            };
            await _tableService.AddTableAsync(table);
        }

        public async Task UpdateTableAsync()
        {
            Console.WriteLine("Enter table id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter table capacity");
            int cpacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter restaurant id");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            var table = new Table
            {
                Capacity = cpacity,
                RestaurantId = restaurantId
            };
            await _tableService.UpdateTableAsync(id, table);
        }

        public async Task DeleteTableAsync()
        {
            Console.WriteLine("Enter table id");
            int tableId = Convert.ToInt32(Console.ReadLine());
            await _tableService.DeleteTableAsync(tableId);
        }

        public async Task ViewAllTablesAsync()
        {
            await _tableService.GetAllTablesAsync();
        }

        public async Task ViewTableByIdAsync()
        {
            Console.WriteLine("Enter table id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _tableService.GetTableByIdAsync(id);
        }
    }
}