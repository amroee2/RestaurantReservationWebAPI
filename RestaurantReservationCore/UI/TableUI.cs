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

        public void DisplayOptions()
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
                    HandleRequest(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void HandleRequest(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    AddTable();
                    break;
                case OperationOptions.Update:
                    UpdateTable();
                    break;
                case OperationOptions.Delete:
                    DeleteTable();
                    break;
                case OperationOptions.View:
                    ViewAllTables();
                    break;
                case OperationOptions.Search:
                    ViewTableById();
                    break;
            }
        }

        public void AddTable()
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
            _tableService.AddTableAsync(table);
        }

        public void UpdateTable()
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
            _tableService.UpdateTableAsync(id, table);
        }

        public void DeleteTable()
        {
            Console.WriteLine("Enter table id");
            int tableId = Convert.ToInt32(Console.ReadLine());
            _tableService.DeleteTableAsync(tableId);
        }

        public void ViewAllTables()
        {
            _tableService.GetAllTablesAsync();
        }

        public void ViewTableById()
        {
            Console.WriteLine("Enter table id:");
            int id = Convert.ToInt32(Console.ReadLine());
            _tableService.GetTableByIdAsync(id);
        }
    }
}
