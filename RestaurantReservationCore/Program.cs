using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;
using RestaurantReservationCore.UI;

while (true)
{
    Console.WriteLine("Welcome to the system!");
    Console.WriteLine("1- Customer\n2- Employee\n3- Menu Item\n4- Reservation\n5- Order\n" +
        "6- Order Item\n7- Restaurant\n8- Table\n0- Exit");

    try
    {
        Enum.TryParse(Console.ReadLine(), out EntityOptions option);
        if (option == EntityOptions.Exit)
        {
            return;
        }
        await HandleRequestAsync(option);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    static async Task HandleRequestAsync(EntityOptions option)
    {
        try
        {
            RestaurantReservationDbContext restaurantReservationDbContext = new RestaurantReservationDbContext();
            switch (option)
            {
                case EntityOptions.Customer:
                    CustomerUI customerUI = new CustomerUI(new CustomerService(new CustomerRepository(restaurantReservationDbContext)));
                    await customerUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.Employee:
                    EmployeeUI employeeUI = new EmployeeUI(new EmployeeService(new EmployeeRepository(restaurantReservationDbContext)));
                    await employeeUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.MenuItem:
                    MenuItemUI menuItemUI = new MenuItemUI(new MenuItemService(new MenuItemRepository(restaurantReservationDbContext)));
                    await menuItemUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.OrderItem:
                    OrderItemUI orderItemUI = new OrderItemUI(new OrderItemService(new OrderItemRepository(restaurantReservationDbContext)));
                    await orderItemUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.Order:
                    OrderUI orderUI = new OrderUI(new OrderService(new OrderRepository(restaurantReservationDbContext)));
                    await orderUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.Reservation:
                    ReservationUI reservationUI = new ReservationUI(new ReservationService(new ReservationRepository(restaurantReservationDbContext)));
                    await reservationUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.Restaurant:
                    RestaurantUI restaurantUI = new RestaurantUI(new RestaurantService(new RestaurantRepository(restaurantReservationDbContext)));
                    await restaurantUI.DisplayOptionsAsync();
                    break;
                case EntityOptions.Table:
                    TableUI tableUI = new TableUI(new TableService(new TableRepository(restaurantReservationDbContext)));
                    await tableUI.DisplayOptionsAsync();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in handling request: {ex.Message}");
        }
    }
}

