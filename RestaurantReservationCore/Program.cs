using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;
using RestaurantReservationCore.UI;

Console.WriteLine("Welcome to the system!");
Console.WriteLine("1- Customer\n2- Employee\n3- Restaruant\n4- Reservation\n5- Order\n" +
    "6- Order Item\n7- Menu Item\n8- Table\n ");

try
{
    Enum.TryParse(Console.ReadLine(), out EntityOptions option);
    HandleRequest(option);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void HandleRequest(EntityOptions option)
{
    RestaurantReservationDbContext restaurantReservationDbContext = new RestaurantReservationDbContext();
    switch (option)
    {
        case EntityOptions.Customer:
            CustomerUI customerUI = new CustomerUI(new CustomerService(new CustomerRepository(restaurantReservationDbContext)));
            customerUI.DisplayOptions();
            break;

    }
}

